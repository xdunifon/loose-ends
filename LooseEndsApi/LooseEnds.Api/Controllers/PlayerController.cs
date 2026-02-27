using LooseEnds.Api.Configuration;
using LooseEnds.Api.Dtos.Players;
using LooseEnds.Api.Dtos.Rounds;
using LooseEnds.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LooseEnds.Api.Controllers;

[Route("game")]
[ApiController]
public class PlayerController(IPlayerService playerService, IRoundService roundService) : BaseController
{
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] JoinRequest req)
    {
        var playerId = await playerService.JoinAsync(req.GameCode, req.Name);

        return Ok(new
        {
            Token = Auth.CreateToken(req.GameCode, playerId, req.Name, UserRole.Player),
            PlayerId = playerId
        });
    }

    [HttpPost("answer"), Authorize(Policy = Policies.Player)]
    public async Task<IActionResult> Answer(AnswerRequest req)
    {
        var gameCode = GetGameCode();
        var playerId = GetUserId();

        await roundService.AnswerAsync(gameCode, playerId, req.ResponseId, req.Answer);

        return Ok();
    }

    [HttpPost("vote"), Authorize(Policy = Policies.Player)]
    public async Task<IActionResult> Vote(VoteRequest req)
    {
        var gameCode = GetGameCode();
        var playerId = GetUserId();

        await roundService.VoteAsync(gameCode, playerId, req.ResponseId);

        return Ok();
    }
}
