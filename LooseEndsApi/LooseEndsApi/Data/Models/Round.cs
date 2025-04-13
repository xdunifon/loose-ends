namespace LooseEndsApi.Data.Models
{
    public class Round
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int GameSessionId { get; set; }

        // Navigation property
        public GameSession GameSession { get; set; }
        public ICollection<RoundPrompt> RoundPrompts { get; set; }
        public ICollection<>
    }
}
