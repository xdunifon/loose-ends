using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record VoteOptionsDto
{
    public int ResponseId { get; init; }
    public required string PlayerId { get; init; }

    public string? Answer { get; init; }

    public static VoteOptionsDto FromEntity(PlayerResponse r) => new()
    {
        ResponseId = r.Id,
        PlayerId = r.PlayerId,
        Answer = r.Answer
    };
}
