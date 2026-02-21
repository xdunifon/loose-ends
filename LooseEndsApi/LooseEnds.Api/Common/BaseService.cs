using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Common;

public abstract class BaseService(GameContext context)
{
    protected readonly GameContext _context = context;

    protected async Task<GameSession> GetSessionByIdAsync(int sessionId)
    {
        var session = await _context.GameSessions
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.Id == sessionId)
            ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");

        return session;
    }

    public void SaveContext()
    {
        _context.SaveChanges();
    }

    public async Task SaveContextAsync()
    {
        await _context.SaveChangesAsync();
    }
}
