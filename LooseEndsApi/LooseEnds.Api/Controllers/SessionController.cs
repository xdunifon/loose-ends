using LooseEnds.Api.Configuration;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[ApiController]
[Route("api/game")]
public class SessionController(ISessionService service) : BaseController
{
    [HttpGet("")]
    [Authorize(Policy = Policies.Host)]
    [Authorize(Policy = Policies.Player)]
    public async Task<IActionResult> Get()
    {
        var gameCode = GetGameCode();

        return Ok(await service.GetAsync(gameCode));
    }
}
