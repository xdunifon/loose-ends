using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace LooseEndsApi.Hubs
{
    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        // (ConnectionId) -> (GameCode, Role, PlayerName)
        private static ConcurrentDictionary<string, (string GameCode, string Role, string PlayerName)> Connections = new();

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Connections.TryRemove(Context.ConnectionId, out var info))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, info.GameCode);
                Console.WriteLine($"{info.PlayerName} disconnected from game {info.GameCode}");
                await Clients.Group(info.GameCode).SendAsync("PlayerDisconnected", info.PlayerName);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGame(string gameCode, string playerName, string role)
        {
            if (string.IsNullOrWhiteSpace(role) || (role != "host" && role != "player"))
            {
                await Clients.Caller.SendAsync("Error", "Invalid role.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
            Connections[Context.ConnectionId] = (gameCode, role, playerName);

            Console.WriteLine($"{playerName} joined game {gameCode} as {role}");
            await Clients.Group(gameCode).SendAsync("PlayerJoined", playerName, role);
        }

        public async Task StartGame()
        {
            if (Connections.TryGetValue(Context.ConnectionId, out var info))
            {
                if (info.Role != "host")
                {
                    await Clients.Caller.SendAsync("Error", "Only the host can start the game.");
                    return;
                }

                Console.WriteLine($"Host started game {info.GameCode}");
                await Clients.Group(info.GameCode).SendAsync("GameStarted");
            }
        }

        public async Task SubmitResponse(int roundPromptId, string answer)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            Console.WriteLine($"{info.PlayerName} submitted a response: {answer}");
            await Clients.Group(info.GameCode).SendAsync("ResponseSubmitted", info.PlayerName, roundPromptId, answer);
        }

        public async Task SubmitVote(int responseId)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            Console.WriteLine($"{info.PlayerName} voted for response {responseId}");
            await Clients.Group(info.GameCode).SendAsync("VoteReceived", info.PlayerName, responseId);
        }

        // Example of sending to just the host
        public async Task SendToHost(string message)
        {
            if (!Connections.TryGetValue(Context.ConnectionId, out var info))
                return;

            foreach (var (connectionId, entry) in Connections)
            {
                if (entry.GameCode == info.GameCode && entry.Role == "host")
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
                if (entry.GameCode == info.GameCode && entry.Role == "player")
                {
                    await Clients.Client(connectionId).SendAsync("ReceivePlayerMessage", message);
                }
            }
        }
    }
}
