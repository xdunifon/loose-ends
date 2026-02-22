using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class Round
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Session")]
    public int SessionId { get; set; }
    public virtual GameSession Session { get; set; } = default!;

    public bool IsCompleted { get; set; } = false;

    public int Number { get; set; }

    [ForeignKey("VotingRoundPrompt")]
    public int? VotingRoundPromptId { get; set; }
    public virtual RoundPrompt? VotingRoundPrompt { get; set; }

    public virtual ICollection<RoundPrompt> RoundPrompts { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public Round(GameSession session, int number)
    {
        Session = session;
        Number = number;
    }

    public RoundPrompt AddPrompt(string content, int durationInSeconds)
    {
        var newRoundPrompt = new RoundPrompt(this, content, durationInSeconds);
        RoundPrompts.Add(newRoundPrompt);
        return newRoundPrompt;
    }
    #endregion
}
