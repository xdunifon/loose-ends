using LooseEnds.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LooseEnds.Database;

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

    public static void SeedData(DbContext context, bool isDev)
    {
        using var stream = File.OpenRead("data/prompts.json");
        var seedPrompts = JsonSerializer.Deserialize<List<Prompt>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (seedPrompts == null) return;

        foreach (var p in seedPrompts)
        {
            if (!context.Set<Prompt>().Any(existing => existing.Content == p.Content))
            {
                context.Set<Prompt>().Add(p);
            }
        }
        context.SaveChanges();
    }
}