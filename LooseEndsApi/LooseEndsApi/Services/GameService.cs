using LooseEndsApi.Data.Models;
using LooseEndsApi.Models.Rounds;
using LooseEndsApi.Services;
using Microsoft.EntityFrameworkCore;
using LooseEndsApi.Models.Prompts;
using LooseEndsApi.Models;
using Microsoft.Extensions.Options;

public class GameService : BaseService
{
    private PlayerService _playerService;
    private RoundService _roundService;
    private readonly GameSettings _settings;

    public GameService(IOptions<GameSettings> options, GameDbContext context, PlayerService playerService, RoundService roundService) : base(context) 
    {
        _settings = options.Value;
        _playerService = playerService;
        _roundService = roundService;
    }

    public async Task<GameSession?> GetGame(string gameCode)
    {
        return await _context.GameSessions.FirstOrDefaultAsync(g => g.GameCode == gameCode);
    }

    public async Task<string> CreateGame()
    {
        var newGame = new GameSession();
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
        if (session.Rounds.Count() >= _settings.NumberOfRounds)
        {
            await CompleteGame(session);
            return null;
        }

        Round newRound = await _roundService.CreateRound(session);
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

    public async Task<Player?> CompleteGame(GameSession session)
    {
        session.IsCompleted = true;
        return await _playerService.GetWinner(session);
    }

    public async Task<Player?> AddPlayer(string gameCode, string playerName)
    {
        GameSession? session = await GetGame(gameCode);
        if (session == null) { throw new Exception("Session couldn't be found"); }

        var player = _playerService.CreatePlayer(session, playerName);
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
        return player;
    }
}


