using LooseEnds.Api.Resources.Sessions;
using Microsoft.AspNetCore.Mvc;

[Route("api/player")]
[ApiController]
public class PlayerController(GameService gameService) : ControllerBase
{
    [HttpPost("join/{gameCode}")]
    public IActionResult JoinGame(string gameCode, [FromBody] string playerName)
    {
        var result = gameService.AddPlayer(gameCode, playerName);
        if (result == null) return NotFound("Game not found");
        return Ok("Player joined");
    }
}
