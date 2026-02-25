using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LooseEnds.Database.Entities;

[PrimaryKey(nameof(PlayerId), nameof(ResponseId))]
public class PlayerVote
{
    public required string PlayerId { get; set; }
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; set; } = default!;

    public int ResponseId { get; set; }
    [ForeignKey(nameof(ResponseId))]
    public virtual PlayerResponse Response { get; set; } = default!;
}
