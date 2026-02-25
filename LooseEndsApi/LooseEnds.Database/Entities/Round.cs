using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class Round
{
    public Round() { }

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Number { get; set; }
    public bool PromptingCompleted { get; set; } = false;
    public bool VotingCompleted { get; set; } = false;
    public DateTime? AnswerDueUtc { get; set; }

    public int SessionId { get; set; }
    [ForeignKey(nameof(SessionId))]
    public virtual GameSession Session { get; set; } = default!;

    // Which round is being actively voted on
    public int? VotingRoundPromptId { get; set; }
    [ForeignKey(nameof(VotingRoundPromptId))]
    public virtual RoundPrompt? VotingRoundPrompt { get; set; }

    [InverseProperty(nameof(RoundPrompt.Round))]
    public virtual ICollection<RoundPrompt> RoundPrompts { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public Round(GameSession session, int number)
    {
        Session = session;
        Number = number;
    }

    public RoundPrompt AddPrompt(string content)
    {
        var newRoundPrompt = new RoundPrompt(this, content);
        RoundPrompts.Add(newRoundPrompt);
        return newRoundPrompt;
    }
    #endregion
}
