using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LooseEnds.Database.Entities;

public class PlayerResponse
{
    public PlayerResponse() { }

    public int Id { get; set; }
    public string? Answer { get; set; }
    public DateTime? SubmittedUtc { get; set; }

    public required string PlayerId { get; set; }
    [ForeignKey(nameof(PlayerId))]
    public virtual Player Player { get; set; } = default!;

    public int PromptId { get; set; }
    [ForeignKey(nameof(PromptId))]
    public virtual RoundPrompt Prompt { get; set; } = default!;

    public virtual ICollection<PlayerVote> Votes { get; set; } = [];

    #region BEHAVIOR
    [SetsRequiredMembers]
    public PlayerResponse(Player player, RoundPrompt prompt)
    {
        Player = player;
        PlayerId = player.PlayerId;
        Prompt = prompt;
    }

    public void AddAnswer(string answer)
    {
        Answer = answer;
    }

    public void AddVote(string playerId)
    {
        if (!Votes.Any(v => v.PlayerId == playerId))
        {
            Votes.Add(new PlayerVote { PlayerId = playerId, Response = this });
        }
    }
    #endregion
}
