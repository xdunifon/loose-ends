using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record RoundDto
{
    public int Number { get; init; }

    public DateTime? AnswerDueUtc { get; init; }
    public bool PromptingCompleted { get; init; }


    public int? ActiveVotingPromptId { get; init; }
    public bool VotingCompleted { get; init; }

    public IEnumerable<PromptDto> Prompts { get; init; } = [];

    public static RoundDto FromEntity(Round r) => new()
    {
        Number = r.Number,
        AnswerDueUtc = r.AnswerDueUtc,
        PromptingCompleted = r.PromptingCompleted,
        ActiveVotingPromptId = r.VotingRoundPromptId,
        VotingCompleted = r.VotingCompleted,

        Prompts = r.RoundPrompts.Select(PromptDto.FromEntity)
    };
}
