using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class PlayerResponse
{
    public int Id { get; set; }

    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public virtual Player Player { get; set; } = default!;

    [ForeignKey("Prompt")]
    public int PromptId { get; set; }
    public virtual RoundPrompt Prompt { get; set; } = default!;

    public string? Answer { get; set; }

    public virtual ICollection<PlayerVote> Votes { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public PlayerResponse(Player player, RoundPrompt prompt)
    {
        Player = player;
        Prompt = prompt;
    }

    public void AddAnswer(string answer)
    {
        Answer = answer;
    }
    #endregion
}
