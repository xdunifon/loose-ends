using LooseEndsApi.Data.Models;

namespace LooseEndsApi.Services
{
    public class PlayerService : BaseService
    {
        public PlayerService(GameDbContext context) : base(context) { }

        public Player CreatePlayer(GameSession session, string playerName)
        {
            return new Player { Name = playerName, GameSession = session };
        }

        public Player? GetWinner(GameSession session)
        {
            Player winner = _context.Players
                .Where(player => player.GameSessionId == session.Id)
                .OrderByDescending(player => player.Points)
                .First();

            if (winner == null) { return null; }

            return winner;
        }
    }
}
