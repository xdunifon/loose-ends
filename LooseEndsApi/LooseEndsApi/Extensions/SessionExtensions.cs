using LooseEndsApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LooseEndsApi.Extensions
{
    public static class SessionExtensions
    {
        public static Round? GetLatestRound(this GameSession session)
        {
            return session.Rounds.OrderByDescending(r => r.Number).FirstOrDefault();
        }
        public static Player? GetRandomPlayer(this GameSession session, List<Player> excludedPlayers)
        {
            Player[] players = excludedPlayers.Count == 0 ? session.Players.ToArray() : session.Players.Where(p => !excludedPlayers.Contains(p)).ToArray();
            
            if (players.Length == 0)
            {
                return null;
            }

            var rng = new Random();
            int ranIndex = rng.Next(0, players.Length);
            return players[ranIndex];
        }
    }
}
