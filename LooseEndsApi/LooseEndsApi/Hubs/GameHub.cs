using LooseEndsApi.Database.Entities;
using LooseEndsApi.Models.Rounds;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LooseEndsApi.Hubs
{
    public enum RoleEnum
    {
        None,
        Player,
        Host
    }

    public class ConnectionData
    {
        public required string GameCode { get; init; }
        public RoleEnum Role { get; init; }
        public required string Name { get; init; }
    }

    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        // (ConnectionId) -> (GameCode, Role, PlayerName)
        private static ConcurrentDictionary<string, ConnectionData> Connections = new();

        //public async Task SendTestData()
        //{
        //    await Clients.All.SendAsync("ReceiveTestData", new { Message = "Hello World", Time = DateTime.Now });
        //}

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Connections.TryRemove(Context.ConnectionId, out ConnectionData? info))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, info.GameCode);
                Console.WriteLine($"{info.Name} disconnected from game {info.GameCode}");
                await Clients.Group(info.GameCode).SendAsync("PlayerDisconnected", info.Name);
            }
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Create the game session in the database.
        /// If successful, add the user to the connection groups.
        /// Return generated code to caller.
        /// </summary>
        /// <returns></returns>
        public async Task CreateGame()
        {
            try
            {
                string newCode = await _gameService.CreateGame();
                await Groups.AddToGroupAsync(Context.ConnectionId, newCode);
                Connections[Context.ConnectionId] = new ConnectionData { GameCode = newCode, Role = RoleEnum.Host, Name = "Host" };

                await Clients.Caller.SendAsync("GameCreated", newCode);
            } 
            catch (Exception e)
            {
                // Send out error
                throw e;
            }
        }

        public async Task JoinGame(string gameCode, string playerName)
        {
            try
            {
                Player? newPlayer = await _gameService.AddPlayer(gameCode, playerName);

                if (newPlayer != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
                    Connections[Context.ConnectionId] = new ConnectionData { GameCode = gameCode, Role = RoleEnum.Player, Name = playerName };

                    Console.WriteLine($"{playerName} joined game {gameCode}");
                    await Clients.Group(gameCode).SendAsync("PlayerJoined", gameCode, playerName);
                }
            } 
            catch (Exception e)
            {
                // Send out error
                throw e;
            }
        }

        public async Task StartGame(string gameCode)
        {
            if (Connections.TryGetValue(Context.ConnectionId, out var info))
            {
                if (info.Role != RoleEnum.Host)
                {
                    await Clients.Caller.SendAsync("Error", "Only the host can start the game.");
                    return;
                }

                GameSession? session = await _gameService.GetGame(gameCode);
                if (session == null) { throw new Exception("Session couldn't be found"); }

                RoundDto? newRound = await _gameService.StartGame(session);
                if (newRound == null) { throw new Exception("Invalid game to start"); }

                Console.WriteLine($"Host started game {info.GameCode}");
                await Clients.Group(info.GameCode).SendAsync("GameStarted", newRound);
            }
        }

        public async Task SubmitResponse(int roundPromptId, string answer)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            Console.WriteLine($"{info.Name} submitted a response: {answer}");
            await Clients.Group(info.GameCode).SendAsync("ResponseSubmitted", info.Name, roundPromptId, answer);
        }

        public async Task SubmitVote(int responseId)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            Console.WriteLine($"{info.Name} voted for response {responseId}");
            await Clients.Group(info.GameCode).SendAsync("VoteReceived", info.Name, responseId);
        }

        // Example of sending to just the host
        public async Task SendToHost(string message)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            foreach (var (connectionId, entry) in Connections)
            {
                if (entry.GameCode == info.GameCode && entry.Role == RoleEnum.Host)
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveHostMessage", message);
                }
            }
        }

        // Example of sending to just the players
        public async Task SendToPlayers(string message)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            foreach (var (connectionId, entry) in Connections)
            {
                if (entry.GameCode == info.GameCode && entry.Role == RoleEnum.Player)
                {
                    await Clients.Client(connectionId).SendAsync("ReceivePlayerMessage", message);
                }
            }
        }
    }
}
