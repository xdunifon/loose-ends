using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LooseEndsApi.Data.Models
{
    public class PlayerVote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int ResponseId { get; set; }

        public virtual Player Player { get; set; }
        public virtual PlayerResponse Response { get; set; }
    }
}
