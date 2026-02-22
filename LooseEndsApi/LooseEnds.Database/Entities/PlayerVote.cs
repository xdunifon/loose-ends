using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LooseEnds.Database.Entities;

public class PlayerVote
{
    public PlayerVote() { }

    [Key, ForeignKey("Player")]
    public int PlayerId { get; set; }
    public virtual Player Player { get; set; } = default!;

    [Key, ForeignKey("Response")]
    public int ResponseId { get; set; }
    public virtual PlayerResponse Response { get; set; } = default!;
}
