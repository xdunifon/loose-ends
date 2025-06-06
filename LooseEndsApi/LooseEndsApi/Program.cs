using LooseEndsApi.Hubs;
using LooseEndsApi.Models;
using LooseEndsApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var connectionString = configuration.GetConnectionString("GameConnection");
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<RoundService>();
builder.Services.AddScoped<PromptService>();

builder.Services.Configure<GameSettings>(builder.Configuration.GetSection("GameSettings"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors();
app.MapHub<GameHub>("/gamehub");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
