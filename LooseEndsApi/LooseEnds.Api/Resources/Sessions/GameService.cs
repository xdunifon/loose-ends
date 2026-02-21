using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LooseEnds.Api.Configuration;
using LooseEnds.Api.Resources.Rounds.Dtos;
using LooseEnds.Api.Common;
using LooseEnds.Api.Resources.Players;
using LooseEnds.Api.Resources.Prompts.Dtos;
using LooseEnds.Database;
using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Resources.Sessions;

public class GameService(IOptions<GameSettings> options, GameContext context, PlayerService playerService) : BaseService(context)
{
    // Get game summary by session code

    public async Task<GameSession?> GetGame(string gameCode)
    {
        return await _context.GameSessions.FirstOrDefaultAsync(g => g.GameCode == gameCode);
    }

    public async Task<string> CreateGame()
    {
        var newGame = new GameSession(30);
        _context.GameSessions.Add(newGame);

        await _context.SaveChangesAsync();
        return newGame.GameCode;
    }

    /// <summary>
    /// Start game session and return the first round.
    /// Null result means that the game was already created
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<RoundDto?> StartGame(GameSession session)
    {
        return await StartNextRoundOrCompleteGame(session);
    }

    public async Task<RoundDto?> StartNextRoundOrCompleteGame(GameSession session)
    {
        if (session.Rounds.Count() >= options.Value.NumberOfRounds)
        {
            await CompleteGame(session);
            return null;
        }

        Round newRound = new(session, session.Rounds.Count + 1);
        _context.Rounds.Add(newRound);
        await SaveContextAsync();

        return new RoundDto
        {
            GameCode = session.GameCode,
            EndDateTime = DateTime.Now.AddSeconds(session.RoundTimer + 1), // Given a buffer of 1 to account for transmission speeds, not sure how this plays out in practice
            Prompts = newRound.RoundPrompts.Select(p => new PromptDto
            {
                AssignedPlayers = p.PlayerResponses.Select(pr => pr.Player.Name).ToArray(),
                Content = p.Prompt
            }).ToArray()
        };
    }

    public async Task CompleteGame(GameSession session)
    {
        session.IsActive = false;
    }

    public async Task<Player?> AddPlayer(string gameCode, string playerName)
    {
        GameSession? session = await GetGame(gameCode);
        if (session == null) { throw new Exception("Session couldn't be found"); }

        var player = new Player(session, playerName);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}


