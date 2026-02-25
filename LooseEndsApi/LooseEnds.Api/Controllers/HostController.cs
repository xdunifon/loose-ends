using LooseEnds.Api.Configuration;
using LooseEnds.Api.Dtos.Sessions;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[ApiController]
[Route("api/game")]
public class HostController(ISessionService service) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        var gameCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
        var hostId = Guid.NewGuid().ToString();
        await service.CreateAsync(gameCode, hostId);

        return Ok(new
        {
            Token = Auth.CreateToken(gameCode, hostId, $"Host{gameCode}", UserRole.Host),
            HostId = hostId
        });
    }

    [HttpPost("start"), Authorize(Policy = Policies.Host)]
    public async Task<IActionResult> Start(StartGameRequest req)
    {
        var gameCode = GetGameCode();
        await service.StartAsync(gameCode, req.RoundDurationInSeconds);

        return Ok();
    }

    [HttpPost("next"), Authorize(Policy = Policies.Host)]
    public async Task<IActionResult> Next()
    {
        var gameCode = GetGameCode();
        await service.NextAsync(gameCode);
        return Ok();
    }
}
