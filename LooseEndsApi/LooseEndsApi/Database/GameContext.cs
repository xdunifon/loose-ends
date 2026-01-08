using LooseEndsApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Collections.Generic;
using System.Configuration;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options) : base(options) { }

    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerResponse> PlayerResponses { get; set; }
    public DbSet<PlayerVote> PlayerVotes { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<RoundPrompt> RoundPrompts { get; set; }
    public DbSet<DefaultResponse> DefaultResponses { get; set; }
}
