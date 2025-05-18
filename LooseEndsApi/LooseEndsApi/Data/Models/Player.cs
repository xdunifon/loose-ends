using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LooseEndsApi.Data.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GameSessionId { get; set; }  // Foreign Key

        public required string Name { get; set; }
        public int Points { get; set; } = 0;

        public virtual GameSession GameSession { get; set; }  // Navigation Property
        public virtual PlayerResponse Responses { get; set; }
        public virtual PlayerVote Votes { get; set; }
        
    }

}
