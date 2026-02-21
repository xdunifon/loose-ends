using LooseEnds.Api.Common;
using LooseEnds.Api.Database;
using LooseEnds.Api.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Resources.Prompts;

public class PromptService : BaseService
{
    public PromptService(GameContext context) : base(context) { }

    public async Task<List<RoundPrompt>> CreateRoundPrompts(GameSession session, Round round)
    {
        var assignedPlayers = new List<Player>();
        int promptCount = (session.Players.Count() + 1) / 2; // Handles odd/even automatically
        var roundPrompts = new List<RoundPrompt>(promptCount);

        for (int i = 0; i < promptCount; i++)
        {
            var promptPlayers = new List<Player>();
            for (int j = 0; j < 2; j++)
            {
                Player? player = session.GetRandomPlayer(assignedPlayers);
                if (player != null)
                {
                    assignedPlayers.Add(player); // add to assigned list so they aren't assigned a second time
                    promptPlayers.Add(player);
                } else
                {
                    // This may change but the current idea is to add a bot player that the DB is seeded with
                    promptPlayers.Add(await _context.Players.FindAsync(0));
                }
            }
            var prompt = await CreateRoundPrompt(round, promptPlayers);
            roundPrompts.Add(prompt);
        }

        return roundPrompts;
    }

    public async Task<RoundPrompt> CreateRoundPrompt(Round round, List<Player> promptPlayers)
    {
        var prompt = await GetRandomPrompt();

        var roundPrompt = new RoundPrompt
        {
            Round = round,
            Prompt = prompt.Content,
        };

        roundPrompt.PlayerResponses = promptPlayers.Select(player => new PlayerResponse
        {
            Player = player,
            RoundPrompt = roundPrompt,
        }).ToList();

        return roundPrompt;
    }


    public async Task<Prompt> GetRandomPrompt()
    {
        var rng = new Random();
        int ranIndex = rng.Next(0, await _context.Prompts.CountAsync());
        List<int> promptIds = await _context.Prompts.Select(prompt => prompt.Id).ToListAsync();
        return await _context.Prompts.FindAsync(promptIds[ranIndex]);
    }

    public async Task<Player?> GetRandomPlayer(int sessionId, List<Player> listToExclude)
    {
        var rng = new Random();
        List<Player> players = await _context.Players.Where(player => player.GameSessionId == sessionId && !listToExclude.Contains(player)).ToListAsync();
        if (players.Count == 0)
        {
            return null;
        }

        int ranIndex = rng.Next(0, players.Count);
        return players[ranIndex];
    }
}
