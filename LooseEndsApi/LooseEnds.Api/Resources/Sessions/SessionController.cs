using LooseEnds.Api.Resources;
using LooseEnds.Api.Resources.Sessions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[Route("api/game")]
[ApiController]
public class SessionController(GameService service, IHubContext<GameHub> hubContext) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateGame()
    {
        var gameCode = service.CreateGame();
        return Ok(new { gameCode });
    }

    [HttpGet("{gameCode}")]
    public IActionResult GetGame(string gameCode)
    {
        var game = service.GetGame(gameCode);
        if (game == null) return NotFound("Game not found");
        return Ok(game);
    }

    [HttpPost("test-hub")]
    public async Task<IActionResult> SendTestHubMethod()
    {
        await hubContext.Clients.All.SendAsync("ReceiveTestData", new { Message = "Hello World", Time = DateTime.Now });
        return Ok("Method sent");
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Hello World");
    }
}
