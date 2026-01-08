using LooseEndsApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEndsApi.Services
{
    public class PlayerService : BaseService
    {
        public PlayerService(GameContext context) : base(context) { }

        public Player CreatePlayer(GameSession session, string playerName)
        {
            return new Player { Name = playerName, GameSession = session };
        }

        public async Task<Player?> GetWinner(GameSession session)
        {
            Player winner = await _context.Players
                .Where(player => player.GameSessionId == session.Id)
                .OrderByDescending(player => player.Points)
                .FirstAsync();

            if (winner == null) { return null; }

            return winner;
        }
    }
}
