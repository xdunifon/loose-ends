using LooseEnds.Api.Common;
using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Resources.Players;

public class PlayerService(GameContext context) : BaseService(context)
{
    public async Task GetPlayersBySessionIdAsync(int sessionId)
    {
        var session = await _context.GameSessions
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.Id == sessionId)
            ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");   
    }
    public Task GetPlayerByIdAsync(int id) => throw new NotImplementedException();
    public Task GetWinningPlayerBySessionIdAsync(int sessionId) => throw new NotImplementedException();

    public async Task<Player?> GetWinner(GameSession session)
    {
        Player winner = await _context.Players
            .Where(player => player.SessionId == session.Id)
            .OrderByDescending(player => player.Points)
            .FirstAsync();

        if (winner == null) { return null; }

        return winner;
    }
}
