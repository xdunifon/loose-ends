using LooseEnds.Api.Resources.Players;
using LooseEnds.Api.Resources.Prompts;
using LooseEnds.Api.Resources.Sessions;
using LooseEnds.Database;
using Microsoft.EntityFrameworkCore;

namespace LooseEnds.Api.Configuration;

public static class ProgramExtensions
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("GameConnection");
        builder.Services.AddDbContext<GameContext>(options => options.UseSqlite(connectionString));
    }

    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<GameService>();
        builder.Services.AddScoped<PlayerService>();
        builder.Services.AddScoped<PromptService>();
    }

    public static void ConfigureGameSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<GameSettings>(
            builder.Configuration.GetSection("GameSettings")
        );
    }
}
