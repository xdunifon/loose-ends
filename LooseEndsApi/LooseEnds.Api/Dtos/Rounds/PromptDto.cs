using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record PromptDto
{
    public int Id { get; init; }
    public required string Prompt { get; init; }

    public DateTime? VoteDueUtc { get; init; }
    public bool IsCompleted { get; init; }

    public IEnumerable<VoteOptionsDto> VoteOptions { get; init; } = [];

    public static PromptDto FromEntity(RoundPrompt p) => new()
    {
        Id = p.Id,
        Prompt = p.Prompt,
        VoteDueUtc = p.VoteDueUtc,
        IsCompleted = p.IsCompleted,

        VoteOptions = p.PlayerResponses.Select(VoteOptionsDto.FromEntity)
    };
}
