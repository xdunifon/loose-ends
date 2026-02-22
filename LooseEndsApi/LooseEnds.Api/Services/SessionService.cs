using Microsoft.Extensions.Options;
using LooseEnds.Api.Configuration;
using LooseEnds.Api.Common;
using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Services;

public interface ISessionService
{
    Task<string> CreateAsync(string gameCode, string hostId);
    Task StartAsync(string gameCode, int roundDurationInSeconds);
    Task NextAsync(string gameCode);
}

public class SessionService(GameContext context, IOptions<GameSettings> options) : BaseService(context), ISessionService
{
    public async Task<string> CreateAsync(string gameCode, string hostId)
    {
        var newGame = new GameSession(hostId, gameCode, options.Value.RoundDurationInSeconds);

        _context.GameSessions.Add(newGame);
        await SaveContextAsync();

        return newGame.GameCode;
    }

    public async Task StartAsync(string gameCode, int roundDurationInSeconds)
    {
        var game = _context.GameSessions
            .Include(s => s.Players)
            .Include(s => s.Rounds)
            .Where(s => s.GameCode == gameCode)
            .FirstOrDefault() ?? throw new NotFoundException($"Couldn't find game with code {gameCode}");

        // Validate that the game can start
        if (game.Rounds.Count > 0) throw new Exception("This game has already been started");
        if (game.Players.Count < 3) throw new Exception("At least two players are required to start");

        game.RoundTimer = roundDurationInSeconds;

        // Add bot if player count is odd
        if (game.Players.Count % 2 != 0)
        {
            var botId = Guid.NewGuid().ToString();
            var bot = game.AddPlayer(botId, $"Bot{gameCode}", isBot: true);
        }

        var promptsPerRound = game.Players.Count / 2;
        var numPrompts = options.Value.NumberOfRounds * promptsPerRound;
        
        // Order by random guids and take first n prompts
        // TODO: Generating a random GUID for every single entry is not very efficient, replace this in the future
        var promptOptions = await _context.Prompts
            .Where(p => p.Active)
            .OrderBy(x => Guid.NewGuid())
            .Take(numPrompts)
            .ToListAsync();

        // Generate rounds
        for(int i = 0; i < options.Value.NumberOfRounds; i++)
        {
            var round = game.AddRound(i + 1);
            
            // Order by random guids and take first n players
            var playerOptions = game.Players.OrderBy(x => Guid.NewGuid()).ToList();

            // Generate prompts for round
            for (int j = 0; j < promptsPerRound; j++)
            {
                var selectedPrompt = promptOptions[0];
                promptOptions.RemoveAt(0);

                var roundPrompt = round.AddPrompt(selectedPrompt.Content);

                // Assign two players
                roundPrompt.AssignPlayer(playerOptions[0]);
                roundPrompt.AssignPlayer(playerOptions[1]);
                playerOptions.RemoveRange(0, 2);
            }
        }

        await SaveContextAsync();
    }

    public async Task NextAsync(string gameCode)
    {
        throw new NotImplementedException();
    }
}

#region REFERENCE

//public async Task<GameSession?> GetGame(string gameCode)
//    {
//        return await _context.GameSessions.FirstOrDefaultAsync(g => g.GameCode == gameCode);
//    }

//    public async Task<string> CreateGame()
//    {
//        var newGame = new GameSession(30);
//        _context.GameSessions.Add(newGame);

//        await _context.SaveChangesAsync();
//        return newGame.GameCode;
//    }

//    /// <summary>
//    /// Start game session and return the first round.
//    /// Null result means that the game was already created
//    /// </summary>
//    /// <param name="session"></param>
//    /// <returns></returns>
//    public async Task<RoundDto?> StartGame(GameSession session)
//    {
//        return await StartNextRoundOrCompleteGame(session);
//    }

//    public async Task<RoundDto?> StartNextRoundOrCompleteGame(GameSession session)
//    {
//        if (session.Rounds.Count() >= options.Value.NumberOfRounds)
//        {
//            await CompleteGame(session);
//            return null;
//        }

//        Round newRound = new(session, session.Rounds.Count + 1);
//        _context.Rounds.Add(newRound);
//        await SaveContextAsync();

//        throw new NotImplementedException();
//        //return new RoundDto
//        //{
//        //    GameCode = session.GameCode,
//        //    EndDateTime = DateTime.Now.AddSeconds(session.RoundTimer + 1), // Given a buffer of 1 to account for transmission speeds, not sure how this plays out in practice
//        //    Prompts = newRound.RoundPrompts.Select(p => new RoundPromptDto
//        //    {
//        //        AssignedPlayers = p.PlayerResponses.Select(pr => pr.Player.Name).ToArray(),
//        //        Content = p.Prompt
//        //    }).ToArray()
//        //};
//    }

//    public async Task CompleteGame(GameSession session)
//    {
//        session.IsActive = false;
//    }

//    public async Task<Player?> AddPlayer(string gameCode, string playerName)
//    {
//        GameSession? session = await GetGame(gameCode);
//        if (session == null) { throw new Exception("Session couldn't be found"); }

//        var player = new Player(session, playerName);
//        _context.Players.Add(player);
//        await _context.SaveChangesAsync();
//        return player;
//    }
#endregion