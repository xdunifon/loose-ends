using LooseEnds.Api.Resources.Prompts.Dtos;

namespace LooseEnds.Api.Resources.Rounds.Dtos;

public class RoundDto
{
    public required string GameCode { get; set; }
    public DateTime EndDateTime { get; set; }
    public required PromptDto[] Prompts { get; set; }
}
