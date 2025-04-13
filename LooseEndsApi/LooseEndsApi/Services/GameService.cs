using LooseEndsApi.Data.Models;
using LooseEndsApi.Services;
using Microsoft.EntityFrameworkCore;

public class GameService : BaseService
{
    private PlayerService _playerService;
    private RoundService _roundService;

    public GameService(GameDbContext context, PlayerService playerService, RoundService roundService) : base(context) 
    {
        _playerService = playerService;
        _roundService = roundService;
    }

    public async Task<GameSession?> GetGame(string gameCode)
    {
        return await _context.GameSessions.FirstOrDefaultAsync(g => g.GameCode == gameCode);
    }

    public async Task<GameSession> CreateGame()
    {
        var newGame = new GameSession();
        _context.GameSessions.Add(newGame);
        await _context.SaveChangesAsync();
        return newGame;
    }

    public async Task<Round?> StartGame(GameSession session)
    {
        session.IsStarted = true;
        return await StartNextRoundOrCompleteGame(session);
    }

    public async Task<Round?> StartNextRoundOrCompleteGame(GameSession session)
    {
        if (session.Rounds.Count >= 3)
        {
            await CompleteGame(session);
            return null;
        }

        Round newRound = _roundService.CreateRound(session);
        _context.Rounds.Add(newRound);
        await SaveContextAsync();

        return newRound;
    }

    public async Task<Player?> CompleteGame(GameSession session)
    {
        session.IsCompleted = true;
        return _playerService.GetWinner(session);
    }

    public async Task<Player> AddPlayer(GameSession session, string playerName)
    {
        var player = _playerService.CreatePlayer(session, playerName);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}


