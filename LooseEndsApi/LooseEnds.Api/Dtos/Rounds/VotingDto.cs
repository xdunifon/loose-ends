using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record VotingDto(int PlayerResponseId, string PromptContent, string Answer)
{
    public static VotingDto FromEntity(PlayerResponse playerResponse, string defaultAnswer)
    {
        return new(
            playerResponse.Id,
            playerResponse.Prompt.Prompt,
            playerResponse.Answer ?? defaultAnswer
        );
    }
}
