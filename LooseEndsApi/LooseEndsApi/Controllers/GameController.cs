using LooseEndsApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Route("api/game")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;
    private readonly IHubContext<GameHub> _hubContext;

    public GameController(GameService gameService, IHubContext<GameHub> gameHubContext)
    {
        _gameService = gameService;
        _hubContext = gameHubContext;
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

    [HttpPost("test-hub")]
    public async Task<IActionResult> SendTestHubMethod()
    {
        await _hubContext.Clients.All.SendAsync("ReceiveTestData", new { Message = "Hello World", Time = DateTime.Now });
        return Ok("Method sent");
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Hello World");
    }
}
