using LooseEnds.Api.Common;
using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Services;

public interface IPromptService
{
    Task<Prompt?> GetRandomPromptAsync(int[]? excludedPromptIds = null);
}

public class PromptService(GameContext context) : BaseService(context), IPromptService
{
    public async Task<Prompt?> GetRandomPromptAsync(int[]? excludedPromptIds)
    {
        var query = _context.Prompts.Where(p => p.Active && !excludedPromptIds.Contains(p.Id));
        
        // Return null if the exclusion list excludes all prompts
        var count = await query.CountAsync();
        if (count == 0) return null;
        
        // Get valid prompt IDs
        var promptIds = await query.Select(p => p.Id).ToArrayAsync();

        // Randomly select
        var rng = new Random();
        var selectedId = promptIds.ElementAt(rng.Next(0, promptIds.Length));

        return await _context.Prompts.FindAsync(selectedId);
    }

    #region REFERENCE
    //public async Task<List<RoundPrompt>> CreateRoundPrompts(GameSession session, Round round)
    //{
    //    var assignedPlayers = new List<Player>();
    //    int promptCount = (session.Players.Count() + 1) / 2; // Handles odd/even automatically
    //    var roundPrompts = new List<RoundPrompt>(promptCount);

    //    for (int i = 0; i < promptCount; i++)
    //    {
    //        var promptPlayers = new List<Player>();
    //        for (int j = 0; j < 2; j++)
    //        {
    //            Player? player = session.GetRandomPlayer(assignedPlayers);
    //            if (player != null)
    //            {
    //                assignedPlayers.Add(player); // add to assigned list so they aren't assigned a second time
    //                promptPlayers.Add(player);
    //            } else
    //            {
    //                // This may change but the current idea is to add a bot player that the DB is seeded with
    //                promptPlayers.Add(await _context.Players.FindAsync(0));
    //            }
    //        }
    //        var prompt = await CreateRoundPrompt(round, promptPlayers);
    //        roundPrompts.Add(prompt);
    //    }

    //    return roundPrompts;
    //}
    #endregion
}
