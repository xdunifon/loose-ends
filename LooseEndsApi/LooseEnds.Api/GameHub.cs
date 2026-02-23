using Microsoft.AspNetCore.SignalR;

namespace LooseEnds.Api;

public static class GameEvents
{
    public const string PlayerJoined = "PlayerJoined";
    public const string PlayerSubmitted = "PlayerSubmitted";
    public const string PlayerVoted = "PlayerVoted";

    public const string GameStarted = "GameStarted";
    public const string GameOver = "GameOver";
    public const string RoundStarted = "RoundStarted";
    public const string PromptingEnded = "PromptingEnded";
    public const string RoundEnded = "RoundEnded";
    public const string VotingStarted = "VotingStarted";
    public const string VotingEnded = "VotingEnded";
}

public class GameHub : Hub
{
    public async Task JoinSession(string gameCode)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);

        // Send notification
        await Clients.Group(gameCode).SendAsync(GameEvents.PlayerJoined, Context.ConnectionId);
    }
}
