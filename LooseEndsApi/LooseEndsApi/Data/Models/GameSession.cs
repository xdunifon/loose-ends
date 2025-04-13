using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace LooseEndsApi.Data.Models
{
    public class GameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsStarted { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public bool InLobby { get; set; } = true;
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;

        public string GameCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

        // Navigation Property
        public List<Player> Players { get; set; } = [];
        public List<Round> Rounds { get; set; } = [];
    }

}
