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

        public Round CreateRound(GameSession session)
        {
            var newRound = new Round()
            {
                GameSession = session,
                RoundPrompts = _promptService.CreateRoundPrompts(session)
            };

            
        }
    }
}
