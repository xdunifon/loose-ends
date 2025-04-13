using LooseEndsApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Collections.Generic;
using System.Configuration;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<RoundPrompt> RoundPrompts { get; set; }
    public DbSet<PlayerResponse> PlayerResponses { get; set; }
    public DbSet<DefaultResponse> DefaultResponses { get; set; }
}
