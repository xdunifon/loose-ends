using LooseEnds.Api.Common;
using LooseEnds.Api.Dtos.Players;
using LooseEnds.Database;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Services;

public interface IPlayerService
{
    Task<string> JoinAsync(string gameCode, JoinRequest req);

    Task<IEnumerable<PlayerDto>> GetBySessionIdAsync(int sessionId);
    Task<PlayerDto> GetByIdAsync(int id);
}

public class PlayerService(GameContext context) : BaseService(context), IPlayerService
{
    public async Task<string> JoinAsync(string gameCode, JoinRequest req)
    {
        var session = await _context.GameSessions
            .Where(s => s.IsActive && s.Rounds.Count == 0)
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.GameCode == gameCode)
            ?? throw new NotFoundException($"Couldn't find an open game with code {gameCode}");

        var playerId = Guid.NewGuid().ToString();
        session.AddPlayer(playerId, req.Name);

        await SaveContextAsync();
        return playerId;
    }

    public async Task<IEnumerable<PlayerDto>> GetBySessionIdAsync(int sessionId)
    {
        var session = await _context.GameSessions
            .Where(s => s.IsActive)
            .Include(s => s.Players)
                .ThenInclude(p => p.Responses)
                    .ThenInclude(r => r.Votes)
            .FirstOrDefaultAsync(s => s.Id == sessionId)
            ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");

        return session.Players.Select(PlayerDto.FromEntity);
    }
    public async Task<PlayerDto> GetByIdAsync(int id)
    {
        var player = await _context.Players
            .Include(p => p.Responses)
                .ThenInclude(r => r.Votes)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Couldn't find player with ID {id}");

        return PlayerDto.FromEntity(player);
    }
}
