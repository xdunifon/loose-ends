using LooseEnds.Api.Common;
using LooseEnds.Api.Resources.Players.Dto;
using LooseEnds.Database;
using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Resources.Players;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetBySessionIdAsync(int sessionId);
    Task<PlayerDto> GetByIdAsync(int id);
}

public class PlayerService(GameContext context) : BaseService(context), IPlayerService
{
    public async Task<IEnumerable<PlayerDto>> GetBySessionIdAsync(int sessionId)
    {
        var session = await GetSessionByIdAsync(sessionId);
        return session.Players.Select(PlayerDto.FromEntity);
    }
    public async Task<PlayerDto> GetByIdAsync(int id)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException($"Couldn't find player with ID {id}");

        return PlayerDto.FromEntity(player);
    }
}
