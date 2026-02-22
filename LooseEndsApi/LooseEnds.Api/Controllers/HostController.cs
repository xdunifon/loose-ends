using LooseEnds.Api.Configuration;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[ApiController]
[Route("api/game")]
public class HostController(ISessionService sessionService) : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        var gameCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
        var hostId = Guid.NewGuid().ToString();
        await sessionService.CreateAsync(gameCode, hostId);

        return Ok(new
        {
            Token = Auth.CreateToken(gameCode, hostId, $"Host{gameCode}", UserRole.Host),
            HostId = hostId
        });
    }

    [HttpPost("start")]
    [Authorize(Policy = "Host")]
    public async Task<IActionResult> Start()
    {
        var gameCode = GetGameCode();
        await sessionService.StartAsync();
        return Ok();
    }
}
