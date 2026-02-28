using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record VoteOptionDto
{
    public int ResponseId { get; init; }
    public required string PlayerId { get; init; }

    public string? Answer { get; init; }

    public static VoteOptionDto FromEntity(PlayerResponse r) => new()
    {
        ResponseId = r.Id,
        PlayerId = r.PlayerId,
        Answer = r.Answer
    };
}
