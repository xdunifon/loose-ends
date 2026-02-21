using LooseEndsApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LooseEndsApi.Database;

public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
{
    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<PlayerResponse> PlayerResponses { get; set; }
    public DbSet<PlayerVote> PlayerVotes { get; set; }
    public DbSet<Prompt> Prompts { get; set; }
    public DbSet<Round> Rounds { get; set; }
    public DbSet<RoundPrompt> RoundPrompts { get; set; }
    public DbSet<DefaultResponse> DefaultResponses { get; set; }
}