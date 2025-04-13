using LooseEndsApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace LooseEndsApi.Services
{
    public class PromptService : BaseService
    {
        public PromptService(GameDbContext context) : base(context) { }

        public async Task<ICollection<RoundPrompt>> CreateRoundPrompts(GameSession session, Round round)
        {
            var assignedPlayers = new List<Player>();
            int promptCount = (session.Players.Count + 1) / 2; // Handles odd/even automatically
            var roundPrompts = new List<RoundPrompt>(promptCount);

            for (int i = 0; i < promptCount; i++)
            {
                var promptPlayers = new List<Player>();
                for (int j = 0; j < 2; j++)
                {
                    Player? player = await GetRandomPlayer(session.Id, assignedPlayers);
                    if (player != null)
                    {
                        assignedPlayers.Add(player);
                        promptPlayers.Add(player);
                    } else
                    {
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
                Prompt = prompt,
                PlayerResponses = new List<PlayerResponse>() // init empty for now
            };

            roundPrompt.PlayerResponses = promptPlayers.Select(player => new PlayerResponse
            {
                Player = player,
                RoundPrompt = roundPrompt
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
}
