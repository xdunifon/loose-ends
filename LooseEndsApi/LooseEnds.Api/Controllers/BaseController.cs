using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LooseEnds.Api.Controllers;

[Route("game")]
[ApiController]
public class BaseController : ControllerBase
{
    protected string GetGameCode() => User.FindFirst("gameCode")?.Value ?? throw new Exception("No game code claim found");
    protected string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("No Name Identifier claim found");
}
