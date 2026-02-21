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
    
    public DateTime EndDateTime { get; set; }
    public required string Prompt { get; set; }

    public virtual ICollection<PlayerResponse> PlayerResponses { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public RoundPrompt(Round round, string content, int durationInSeconds)
    {
        Round = round;
        Prompt = content;
        EndDateTime = DateTime.UtcNow.AddSeconds(durationInSeconds);
    }
    #endregion
}
