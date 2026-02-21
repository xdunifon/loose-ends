using LooseEndsApi.Models.Prompts;

namespace LooseEndsApi.Models.Rounds;

public class VotingDto
{
    public string GameCode { get; set; }
    public DateTime EndDateTime { get; set; }
    public required PromptDto[] Prompts { get; set; }
}
