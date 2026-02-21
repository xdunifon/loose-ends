namespace LooseEnds.Database.Entities;

public class Round
{
    public int Id { get; set; }
    public int GameSessionId { get; set; }

    public bool IsCompleted { get; set; } = false;
    public bool IsVoting { get; set; } = false;

    public int Number { get; set; }

    // Navigation property
    public virtual GameSession GameSession { get; set; }
    public virtual List<RoundPrompt> RoundPrompts { get; set; }
}
