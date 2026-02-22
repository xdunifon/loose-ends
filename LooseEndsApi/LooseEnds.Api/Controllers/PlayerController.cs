using LooseEnds.Api.Configuration;
using LooseEnds.Api.Dtos.Players;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(IPlayerService playerService) : BaseController
{
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] JoinRequest req)
    {
        var gameCode = GetGameCode();
        var playerId = await playerService.JoinAsync(gameCode, req);

        return Ok(new
        {
            Token = Auth.CreateToken(gameCode, playerId, req.Name, UserRole.Player),
            PlayerId = playerId
        });
    }

    [HttpPost("answer")]
    [Authorize(Policy = Policies.Player)]
    public async Task<IActionResult> Answer()
    {
        var gameCode = GetGameCode();
        var playerId = GetUserId();

        return Ok();
    }

    [HttpPost("vote")]
    [Authorize(Policy = Policies.Player)]
    public async Task<IActionResult> Vote()
    {
        return Ok();
    }
}
