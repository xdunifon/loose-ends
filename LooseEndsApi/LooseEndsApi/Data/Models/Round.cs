namespace LooseEndsApi.Data.Models
{
    public class Round
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int GameSessionId { get; set; }

        // Navigation property
        public virtual GameSession GameSession { get; set; }
        public virtual List<RoundPrompt> RoundPrompts { get; set; }
    }
}
