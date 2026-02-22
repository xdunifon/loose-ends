using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record PlayerRoundDto(int Id, int Number, bool IsCompleted, bool IsVoting, RoundPromptDto Prompt)
{
    public static PlayerRoundDto FromEntity(RoundPrompt entity)
    {
        return new(
            entity.RoundId,
            entity.Round.Number,
            entity.Round.IsCompleted,
            entity.Round.VotingRoundPromptId.HasValue,
            RoundPromptDto.FromEntity(entity)
        );
    }
}
