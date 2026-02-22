using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class Player
{
    public Player() { }

    [Key]
    public required string PlayerId { get; set; }

    [ForeignKey("Session")]
    public int SessionId { get; set; }
    public virtual GameSession Session { get; set; } = default!;

    public required string Name { get; set; }
    public virtual ICollection<PlayerResponse> Responses { get; set; } = [];
    public virtual ICollection<PlayerVote> Votes { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public Player(string id, GameSession session, string name)
    {
        PlayerId = id;
        Session = session;
        Name = name;
    }

    public PlayerResponse CreateResponse(RoundPrompt prompt)
    {
        var response = new PlayerResponse(this, prompt);
        Responses.Add(response);
        return response;
    }
    #endregion
}
