using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record RoundDto(int Id, int Number, bool IsCompleted, bool IsVoting)
{
    public static RoundDto FromEntity(Round round)
    {
        return new(round.Id, round.Number, round.IsCompleted, round.VotingRoundPromptId.HasValue);
    }
}
