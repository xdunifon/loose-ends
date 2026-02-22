using LooseEnds.Api.Resources.Players.Dto;
using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Resources.Rounds.Dtos;

public record RoundPromptDto(int RoundPromptId, DateTime EndDateUtc, IEnumerable<PlayerDto> AssignedPlayers, string PromptContent)
{
    public static RoundPromptDto FromEntity(RoundPrompt entity)
    {
        return new(entity.Id, entity.EndDateTime, entity.PlayerResponses.Select(pr => PlayerDto.FromEntity(pr.Player)), entity.Prompt);
    }
}
