using Microsoft.AspNetCore.SignalR;

namespace LooseEnds.Api.Resources;

public static class GameEvents
{
    public const string PlayerJoined = "PlayerJoined";
    public const string GameStarted = "GameStarted";
    public const string RoundStarted = "RoundStarted";
    public const string PlayerSubmitted = "PlayerSubmitted";
    public const string PlayerVoted = "PlayerVoted";
}

public class GameHub : Hub
{

    public async Task JoinSession(string sessionCode)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionCode);

        // Send notification
        await Clients.Group(sessionCode).SendAsync(GameEvents.PlayerJoined, Context.ConnectionId);
    }

    // TODO: MOVE THIS TO AN API ENDPOINT

    /// <summary>
    /// Create the game session in the database.
    /// If successful, add the user to the connection groups.
    /// Return generated code to caller.
    /// </summary>
    /// <returns></returns>
    //public async Task CreateGame()
    //{
    //    try
    //    {
    //        string newCode = await gameService.CreateGame();
    //        await Groups.AddToGroupAsync(Context.ConnectionId, newCode);
    //        Connections[Context.ConnectionId] = new ConnectionData { GameCode = newCode, Role = RoleEnum.Host, Name = "Host" };

    //        await Clients.Caller.SendAsync("GameCreated", newCode);
    //    } 
    //    catch (Exception e)
    //    {
    //        // Send out error
    //        throw e;
    //    }
    //}

    // TODO: MOVE THIS TO AN API ENDPOINT
    //public async Task JoinGame(string gameCode, string playerName)
    //{
    //    try
    //    {
    //        Player? newPlayer = await gameService.AddPlayer(gameCode, playerName);

    //        if (newPlayer != null)
    //        {
    //            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
    //            Connections[Context.ConnectionId] = new ConnectionData { GameCode = gameCode, Role = RoleEnum.Player, Name = playerName };

    //            Console.WriteLine($"{playerName} joined game {gameCode}");
    //            await Clients.Group(gameCode).SendAsync("PlayerJoined", gameCode, playerName);
    //        }
    //    } 
    //    catch (Exception e)
    //    {
    //        // Send out error
    //        throw e;
    //    }
    //}

    // TODO: MOVE THIS TO AN API ENDPOINT
    //public async Task StartGame(string gameCode)
    //{
    //    if (Connections.TryGetValue(Context.ConnectionId, out var info))
    //    {
    //        if (info.Role != RoleEnum.Host)
    //        {
    //            await Clients.Caller.SendAsync("Error", "Only the host can start the game.");
    //            return;
    //        }

    //        GameSession? session = await gameService.GetGame(gameCode);
    //        if (session == null) { throw new Exception("Session couldn't be found"); }

    //        RoundDto? newRound = await gameService.StartGame(session);
    //        if (newRound == null) { throw new Exception("Invalid game to start"); }

    //        Console.WriteLine($"Host started game {info.GameCode}");
    //        await Clients.Group(info.GameCode).SendAsync("GameStarted", newRound);
    //    }
    //}

    //public async Task SubmitResponse(int roundPromptId, string answer)
    //{
    //    if (!Connections.TryGetValue(Context.ConnectionId, out var info))
    //        return;

    //    Console.WriteLine($"{info.Name} submitted a response: {answer}");
    //    await Clients.Group(info.GameCode).SendAsync("ResponseSubmitted", info.Name, roundPromptId, answer);
    //}

    //public async Task SubmitVote(int responseId)
    //{
    //    if (!Connections.TryGetValue(Context.ConnectionId, out var info))
    //        return;

    //    Console.WriteLine($"{info.Name} voted for response {responseId}");
    //    await Clients.Group(info.GameCode).SendAsync("VoteReceived", info.Name, responseId);
    //}
}
