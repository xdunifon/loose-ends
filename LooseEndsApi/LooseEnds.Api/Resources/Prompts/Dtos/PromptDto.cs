namespace LooseEnds.Api.Resources.Prompts.Dtos;

public class PromptDto
{
    public int PromptId { get; set; }
    public required string[] AssignedPlayers { get; set; }
    public required string Content { get; set; }
}
