using LooseEnds.Api.Common;
using LooseEnds.Api.Dtos.Players;
using LooseEnds.Database;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Services;

public interface IPlayerService
{
    Task<string> JoinAsync(string gameCode, string name);
}

public class PlayerService(GameContext context, IHubContext<GameHub> hub) : BaseService(context), IPlayerService
{
    public async Task<string> JoinAsync(string gameCode, string name)
    {
        var session = await _context.GameSessions
            .Where(s => s.IsActive && s.Rounds.Count == 0)
            .Include(s => s.Players)
            .FirstOrDefaultAsync(s => s.GameCode == gameCode)
            ?? throw new NotFoundException($"Couldn't find an open game with code {gameCode}");

        var playerId = Guid.NewGuid().ToString();
        var player = session.AddPlayer(playerId, name);

        await SaveContextAsync();
        await hub.Clients.Group(gameCode).SendAsync(GameEvents.PlayerJoined, PlayerDto.FromEntity(player));
        return playerId;
    }
}
