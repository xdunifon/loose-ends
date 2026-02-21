using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class Player
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Session")]
    public int SessionId { get; set; }
    public virtual GameSession Session { get; set; } = default!;

    public required string Name { get; set; }
    public int Points { get; set; } = 0;

    public virtual ICollection<PlayerResponse> Responses { get; set; } = [];
    public virtual ICollection<PlayerVote> Votes { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public Player(GameSession session, string name)
    {
        Session = session;
        Name = name;
        Points = 0;
    }

    public int AddPoints(int points)
    {
        Points += points;
        return Points;
    }

    public PlayerResponse CreateResponse(RoundPrompt prompt)
    {
        var response = new PlayerResponse(this, prompt);
        Responses.Add(response);
        return response;
    }
    #endregion
}
