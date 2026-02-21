namespace LooseEnds.Database.Entities;

public class PlayerResponse
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int RoundPromptId { get; set; }
    public string? Answer { get; set; }  // nullable answer

    // Navigation properties
    public virtual Player Player { get; set; }
    public virtual RoundPrompt RoundPrompt { get; set; }
    public virtual List<PlayerVote> Votes { get; set; } = new List<PlayerVote>();
}
