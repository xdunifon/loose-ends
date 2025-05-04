using LooseEndsApi.Data.Models;

namespace LooseEndsApi.Services
{
    public class RoundService : BaseService
    {
        private PromptService _promptService;

        public RoundService(GameDbContext context, PromptService promptService) : base(context) 
        {
            _promptService = promptService;
        }

        public async Task<Round> CreateRound(GameSession session)
        {

            var newRound = new Round()
            {
                GameSession = session,
            };
            newRound.RoundPrompts = await _promptService.CreateRoundPrompts(session, newRound);
            return newRound;
        }
    }
}
