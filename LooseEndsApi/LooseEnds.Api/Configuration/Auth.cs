using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LooseEnds.Api.Configuration;

public static class UserRole
{
    public const string Player = "Player";
    public const string Host = "Host";
}

public static class Policies
{
    public const string Player = "Player";
    public const string Host = "Host";
}

public class Auth
{
    private static readonly byte[] KEY = Encoding.ASCII.GetBytes("djfjioaweUIOSDFH&sj8awme7vfhfarh");

    public static void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(KEY),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Policies.Host, policy => policy.RequireClaim("role", UserRole.Host))
            .AddPolicy(Policies.Player, policy => policy.RequireClaim("role", UserRole.Player));
    }

    public static string CreateToken(string gameCode, string identifier, string name, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("gameCode", gameCode),
                new Claim(ClaimTypes.NameIdentifier, identifier),
                new Claim(ClaimTypes.Name, name),
                new Claim("role", role)
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KEY), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
