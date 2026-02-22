using LooseEnds.Api.Common;
using LooseEnds.Api.Dtos.Rounds;
using LooseEnds.Database;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Services;

public interface IRoundService
{
    //Task<RoundDto> GetByIdAsync(int id);
    //Task<IEnumerable<RoundDto>> GetBySessionIdAsync(int sessionId);

    //Task<IEnumerable<RoundPromptDto>> GetPromptsByRoundAsync(int roundId);
    //Task<RoundPromptDto> GetPromptByIdAsync(int roundPromptId);
    //Task AnswerPromptAsync(AnswerPromptRequest req);

    //Task<IEnumerable<VotingDto>?> GetVotingOptionsAsync(int sessionId, int playerId);
    //Task VoteAsync(int sessionId, int responseId, int playerId);
}

public class RoundService(GameContext context, IHubContext<GameHub> hub) : BaseService(context), IRoundService
{

}

#region REFERENCE
//public async Task<RoundDto> GetByIdAsync(int id)
//{
//    var round = await _context.Rounds.FirstOrDefaultAsync(r => r.Id == id)
//        ?? throw new NotFoundException($"Couldn't find round with ID {id}");

//    return RoundDto.FromEntity(round);
//}
//public async Task<IEnumerable<RoundDto>> GetBySessionIdAsync(int sessionId)
//{
//    var session = await _context.GameSessions
//        .Where(s => s.IsActive)
//        .Include(s => s.Rounds)
//        .FirstOrDefaultAsync(s => s.Id == sessionId)
//        ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");

//    return session.Rounds.Select(RoundDto.FromEntity);
//}

//public async Task<RoundPromptDto> GetPromptByIdAsync(int roundPromptId)
//{
//    var prompt = await _context.RoundPrompts
//        .Include(s => s.PlayerResponses)
//            .ThenInclude(pr => pr.Player)
//                .ThenInclude(p => p.Responses)
//                    .ThenInclude(r => r.Votes)
//        .FirstOrDefaultAsync(rp => rp.Id == roundPromptId)
//        ?? throw new NotFoundException($"Couldn't find round prompt with ID {roundPromptId}");

//    return RoundPromptDto.FromEntity(prompt);
//}
//public async Task<IEnumerable<RoundPromptDto>> GetPromptsByRoundAsync(int roundId)
//{
//    var round = await _context.Rounds
//        .Include(r => r.RoundPrompts)
//            .ThenInclude(rp => rp.PlayerResponses)
//                .ThenInclude(pr => pr.Player)
//        .FirstOrDefaultAsync(s => s.Id == roundId)
//        ?? throw new NotFoundException($"Couldn't find round with ID {roundId}");

//    return round.RoundPrompts.Select(RoundPromptDto.FromEntity);
//}

//// This needs some work
//public async Task AnswerPromptAsync(AnswerPromptRequest req)
//{
//    var playerResponse = await _context.PlayerResponses
//        .FirstOrDefaultAsync(pr => pr.Id == req.PlayerResponseId)
//        ?? throw new Exception();

//    playerResponse.AddAnswer(req.Answer);
//    await SaveContextAsync();

//    await hub.Clients.Group(req.gameCode).SendAsync(GameEvents.PlayerSubmitted, req.PlayerId);
//}

//// This needs some work still I think
//public async Task<IEnumerable<VotingDto>?> GetVotingOptionsAsync(int sessionId, int playerId)
//{
//    var session = _context.GameSessions
//        .Where(s => s.IsActive)
//        .Include(s => s.Rounds)
//            .ThenInclude(r => r.RoundPrompts.Where(p => !p.PlayerResponses.Any(pr => pr.PlayerId == playerId)))
//        .FirstOrDefault(s => s.Id == sessionId)
//        ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");

//    var round = session.Rounds
//        .FirstOrDefault(r => r.VotingRoundPromptId.HasValue);

//    if (round == null || !round.VotingRoundPromptId.HasValue) return null;

//    return round.VotingRoundPrompt!.PlayerResponses.Select(p => VotingDto.FromEntity(p, ""));
//}
//public async Task VoteAsync(int sessionId, int responseId, int playerId)
//{
//    var session = _context.GameSessions
//        .Where(s => s.IsActive)
//        .FirstOrDefault(s => s.Id == sessionId)
//        ?? throw new NotFoundException($"Couldn't find session with ID {sessionId}");

//    var response = await _context.PlayerResponses
//        .Include(pr => pr.Player)
//        .FirstOrDefaultAsync(pr => pr.Id == responseId)
//        ?? throw new NotFoundException($"Couldn't find response with ID {responseId}");

//    response.AddVote(playerId);

//    await SaveContextAsync();

//    await hub.Clients.Group(session.GameCode).SendAsync(GameEvents.PlayerVoted, playerId);
//}
#endregion
