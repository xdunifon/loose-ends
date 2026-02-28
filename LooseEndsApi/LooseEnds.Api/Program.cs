using LooseEnds.Api;
using LooseEnds.Api.Configuration;
using LooseEnds.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);
builder.Services.AddControllers();

GlobalExceptionHandler.Configure(builder);
Auth.Configure(builder);
GameSettings.Configure(builder);
builder.ConfigureDatabase();
builder.RegisterServices();
builder.ConfigureCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run migrate on startup so code is self migrating when deploying
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameContext>();
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
app.MapHub<GameHub>("/hub");

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
