using LooseEnds.Api.Dtos.Players;
using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Rounds;

public record RoundPromptDto(int RoundPromptId, IEnumerable<PlayerDto> AssignedPlayers, string PromptContent)
{
    public static RoundPromptDto FromEntity(RoundPrompt entity)
    {
        return new(entity.Id, entity.PlayerResponses.Select(pr => PlayerDto.FromEntity(pr.Player)), entity.Prompt);
    }
}
