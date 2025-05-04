namespace LooseEndsApi.Data.Models
{
    public class RoundPrompt
    {
        public int Id { get; set; }
        public int RoundId { get; set; }
        public int PromptId { get; set; }

        // Navigation properties
        public virtual Round Round { get; set; }
        public virtual Prompt Prompt { get; set; }
        public virtual List<PlayerResponse> PlayerResponses { get; set; }
    }
}
