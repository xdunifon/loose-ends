using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record VotingDto(int PlayerResponseId, string PromptContent, string Answer, DateTime EndDateUtc)
{
    public static VotingDto FromEntity(PlayerResponse playerResponse, string defaultAnswer)
    {
        return new(
            playerResponse.Id,
            playerResponse.Prompt.Prompt,
            playerResponse.Answer ?? defaultAnswer,
            playerResponse.Prompt.EndDateTime
        );
    }
}
