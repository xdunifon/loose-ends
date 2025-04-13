using Microsoft.AspNetCore.Mvc;

[Route("api/game")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("create")]
    public IActionResult CreateGame()
    {
        var gameCode = _gameService.CreateGame();
        return Ok(new { gameCode });
    }

    [HttpGet("{gameCode}")]
    public IActionResult GetGame(string gameCode)
    {
        var game = _gameService.GetGame(gameCode);
        if (game == null) return NotFound("Game not found");
        return Ok(game);
    }
}
