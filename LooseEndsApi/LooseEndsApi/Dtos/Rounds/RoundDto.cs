using LooseEndsApi.Models.Prompts;

namespace LooseEndsApi.Models.Rounds;

public class RoundDto
{
    public required string GameCode { get; set; }
    public DateTime EndDateTime { get; set; }
    public required PromptDto[] Prompts { get; set; }
}
