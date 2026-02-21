namespace LooseEndsApi.Database.Entities;

public class RoundPrompt
{
    public int Id { get; set; }
    public int RoundId { get; set; }
    
    public DateTime EndDateTime { get; set; }
    public string Prompt { get; set; }

    // Navigation properties
    public virtual Round Round { get; set; }
    public virtual List<PlayerResponse> PlayerResponses { get; set; } = new List<PlayerResponse>();
}
