using LooseEnds.Api.Configuration;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[ApiController]
[Route("game")]
public class SessionController(ISessionService service) : BaseController
{
    [HttpGet("")]
    [Authorize(Roles = $"{UserRole.Host}, {UserRole.Player}")]
    public async Task<IActionResult> Get()
    {
        var gameCode = GetGameCode();
        var isHost = IsHost();
        var userId = GetUserId();

        return Ok(await service.GetAsync(gameCode, isHost, userId));
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        return Ok(DateTime.UtcNow.ToShortDateString());
    }
}
