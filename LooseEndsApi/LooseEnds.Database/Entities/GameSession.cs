using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

/**
 * States:
 *  1. Lobby (players can join, but game has not started)
 *  2. Prompting (game has started, players can no longer join but can reconnect)
 *  3. Voting (players can vote on prompts, but can no longer submit answers)
 *  4. Completed (game has ended, players can no longer join)
 */

public enum GameStatus
{
    InActive = 0,
    Lobby = 1,
    Prompting = 2,
    Voting = 3,
    Completed = 4
}

public class GameSession
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public string HostId { get; set; }
    public string GameCode { get; set; }
    public int RoundTimer { get; set; }
    
    public virtual List<Player> Players { get; set; } = [];
    public virtual List<Round> Rounds { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public GameSession(string hostId, string gameCode, int roundDurationInSeconds) 
    {
        HostId = hostId;
        GameCode = gameCode;
        RoundTimer = roundDurationInSeconds;
    }

    public GameStatus Status()
    {
        if (!IsActive) return GameStatus.InActive;
        if (Rounds.Count == 0) return GameStatus.Lobby;

        var currentRound = GetLatestRound();
        if (currentRound == null || currentRound.IsCompleted)
        {
            return GameStatus.Lobby;
        } else if (currentRound.VotingRoundPromptId.HasValue)
        {
            return GameStatus.Voting;
        } else
        {
            return GameStatus.Prompting;
        }
    }

    public Player AddPlayer(string id, string name, bool isBot = false)
    {
        var player = new Player(id, this, name);
        if (isBot) player.IsBot = true;
        Players.Add(player);
        return player;
    }

    public Player? GetRandomPlayer(IEnumerable<Player> excludedPlayers)
    {
        var players = excludedPlayers.Any() ? Players.Where(p => !excludedPlayers.Contains(p)) : Players;

        if (!players.Any()) return null;

        var rng = new Random();
        int ranIndex = rng.Next(0, players.Count());
        return players.ElementAt(ranIndex);
    }

    public Round AddRound(int roundNumber)
    {
        var round = new Round(this, roundNumber);
        Rounds.Add(round);
        return round;
    }
    public Round? GetNextRound()
    {
        return Rounds
            .Where(r => !r.IsCompleted)
            .OrderBy(r => r.Number)
            .First();
    }
    #endregion
}
