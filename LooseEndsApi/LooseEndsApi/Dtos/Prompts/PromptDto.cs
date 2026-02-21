namespace LooseEndsApi.Models.Prompts;

public class PromptDto
{
    public int PromptId { get; set; }
    public required string[] AssignedPlayers { get; set; }
    public required string Content { get; set; }
}
