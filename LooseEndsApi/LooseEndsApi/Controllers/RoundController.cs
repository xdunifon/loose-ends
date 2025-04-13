using Microsoft.AspNetCore.Mvc;

[Route("api/rounds")]
[ApiController]
public class RoundController : ControllerBase
{
    private readonly GameService _gameService;

    public RoundController(GameService gameService)
    {
        _gameService = gameService;
    }
}
