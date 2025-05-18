﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace LooseEndsApi.Data.Models
{
    public class GameSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;

        public string GameCode { get; set; } = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();
        public int RoundTimer { get; set; } = 30;

        // Navigation Property
        public virtual List<Player> Players { get; set; } = new();
        public virtual List<Round> Rounds { get; set; } = new();
    }

}
