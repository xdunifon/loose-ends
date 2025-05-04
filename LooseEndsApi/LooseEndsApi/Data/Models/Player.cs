using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LooseEndsApi.Data.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string Name { get; set; }
        public int Points { get; set; } = 0;

        public int GameSessionId { get; set; }  // Foreign Key
        public virtual GameSession GameSession { get; set; }  // Navigation Property
    }

}
