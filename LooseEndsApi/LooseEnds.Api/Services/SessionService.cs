using Microsoft.Extensions.Options;
using LooseEnds.Api.Configuration;
using LooseEnds.Api.Common;
using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using LooseEnds.Api.Dtos.Rounds;
using LooseEnds.Api.Dtos.Players;

namespace LooseEnds.Api.Services;

public interface ISessionService
{
    Task<string> CreateAsync(string gameCode, string hostId);
    Task StartAsync(string gameCode, int roundDurationInSeconds);
    Task NextAsync(string gameCode);
}

public class SessionService(GameContext context, IOptions<GameSettings> options, IHubContext<GameHub> hub) : BaseService(context), ISessionService
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
            .FirstOrDefault() ?? throw GameExceptions.GameNotFound(gameCode);

        // Validate that the game can start
        if (game.Rounds.Count > 0) throw GameExceptions.AlreadyStarted(gameCode);
        if (game.Players.Count < 3) throw GameExceptions.ThreeRequired();

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
        var game = await _context.GameSessions
            .Where(s => s.IsActive)
            .Include(s => s.Rounds)
            .FirstOrDefaultAsync(s => s.GameCode == gameCode)
            ?? throw GameExceptions.GameNotFound(gameCode);

        var nextRound = game.GetNextRound();
        if (nextRound == null) // All rounds completed, game is over
        {
            var players = await _context.Players
                .Include(p => p.Responses)
                    .ThenInclude(r => r.Votes)
                .Where(p => p.SessionId == game.Id)
                .ToListAsync();

            var dto = players.Select(PlayerScoreDto.FromEntity);

            await hub.Clients.Group(gameCode).SendAsync(GameEvents.GameOver);
        }
        else if (!nextRound.AnswerDueUtc.HasValue) // Round has not started, start it
        {
            // Add buffer second for processing time
            nextRound.AnswerDueUtc = DateTime.Now.AddSeconds(game.RoundTimer + 1);
            await SaveContextAsync();

            // Notify users
            var noti = new RoundStartedDto(nextRound.Number, nextRound.AnswerDueUtc.Value);
            await hub.Clients.Group(gameCode).SendAsync(GameEvents.RoundStarted, noti);
        }
        else if (nextRound.AnswerDueUtc.HasValue) // Round has at least started
        {
            if (!nextRound.VoteDueUtc.HasValue)
            {
                // Prompting is still active, end early
            }
            else if (nextRound.VoteDueUtc.HasValue)
            {
                // Voting is still active, end early
            }
        }
    }
}
