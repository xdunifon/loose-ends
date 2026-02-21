namespace LooseEndsApi.Database.Entities;

public class Prompt
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
    public required string Content { get; set; }
}
