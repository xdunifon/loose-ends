using LooseEnds.Api.Common;
using LooseEnds.Api.Database;
using LooseEnds.Api.Database.Entities;
using LooseEnds.Api.Resources.Prompts;

namespace LooseEnds.Api.Resources.Rounds;

public class RoundService(PromptService promptService, GameContext context) : BaseService(context)
{
    public async Task<Round> CreateRound(GameSession session)
    {
        var newRound = new Round()
        {
            GameSession = session,
            Number = session.GetLatestRound()?.Number + 1 ?? 1
        };
        newRound.RoundPrompts = await promptService.CreateRoundPrompts(session, newRound);
        return newRound;
    }
}
