using Microsoft.AspNetCore.Mvc;

[Route("api/player")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly GameService _gameService;

    public PlayerController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("join/{gameCode}")]
    public IActionResult JoinGame(string gameCode, [FromBody] string playerName)
    {
        var result = _gameService.AddPlayer(gameCode, playerName);
        if (result == null) return NotFound("Game not found");
        return Ok("Player joined");
    }
}
