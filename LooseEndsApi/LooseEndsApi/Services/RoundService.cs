using LooseEndsApi.Database.Entities;

namespace LooseEndsApi.Services
{
    public class RoundService : BaseService
    {
        private PromptService _promptService;

        public RoundService(GameContext context, PromptService promptService) : base(context) 
        {
            _promptService = promptService;
        }

        public async Task<Round> CreateRound(GameSession session)
        {
            var newRound = new Round()
            {
                GameSession = session,
                Number = session.GetLatestRound()?.Number + 1 ?? 1
            };
            newRound.RoundPrompts = await _promptService.CreateRoundPrompts(session, newRound);
            return newRound;
        }
    }
}
