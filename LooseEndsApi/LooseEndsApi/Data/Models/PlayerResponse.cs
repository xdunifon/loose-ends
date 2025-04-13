namespace LooseEndsApi.Data.Models
{
    public class PlayerResponse
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int RoundPromptId { get; set; }
        public string? Answer { get; set; }  // nullable answer
        public int Votes { get; set; } = 0;

        // Navigation properties
        public required Player Player { get; set; }
        public required RoundPrompt RoundPrompt { get; set; }
    }
}
