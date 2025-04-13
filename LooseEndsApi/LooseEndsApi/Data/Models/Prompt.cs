namespace LooseEndsApi.Data.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public required string Content { get; set; }

        // Navigation property
        public ICollection<RoundPrompt> RoundPrompts { get; set; }
    }
}
