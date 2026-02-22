using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class RoundPrompt
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Round")]
    public int RoundId { get; set; }
    public virtual Round Round { get; set; } = default!;
    public required string Prompt { get; set; }

    public virtual ICollection<PlayerResponse> PlayerResponses { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public RoundPrompt(Round round, string content)
    {
        Round = round;
        Prompt = content;
    }

    public PlayerResponse AssignPlayer(Player player)
    {
        var pr = new PlayerResponse(player, this);
        PlayerResponses.Add(pr);
        return pr;
    }

    public void AssignPlayers(ICollection<Player> players)
    {
        foreach (var player in players)
        {
            AssignPlayer(player);
        }
    }
    #endregion
}
