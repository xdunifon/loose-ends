namespace LooseEndsApi.Data.Models
{
    public class RoundPrompt
    {
        public int Id { get; set; }
        public int RoundId { get; set; }
        public int PromptId { get; set; }

        // Navigation properties
        public required Round Round { get; set; }
        public required Prompt Prompt { get; set; }
        public ICollection<PlayerResponse> PlayerResponses { get; set; }
    }
}
