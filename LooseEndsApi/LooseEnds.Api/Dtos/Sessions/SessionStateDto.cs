using LooseEnds.Api.Dtos.Players;
using LooseEnds.Api.Dtos.Rounds;
using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Sessions;

public record SessionStateDto
{
    public required string GameCode { get; init; }
    public DateTime DateCreatedUtc { get; init; }
    public bool IsHost { get; init; }
    public string UserId { get; init; }
    public int PromptingDuration { get; init; }
    public int VotingDuration { get; init; }

    public IEnumerable<PlayerDto> Players { get; init; } = [];
    public IEnumerable<RoundDto> Rounds { get; init; } = [];

    public static SessionStateDto FromEntity(GameSession game, bool isHost, string userId) => new()
    {
        GameCode = game.GameCode,
        DateCreatedUtc = game.DateCreatedUtc,
        IsHost = isHost,
        UserId = userId,
        PromptingDuration = game.RoundTimer,
        VotingDuration = 15,
        
        Players = game.Players.Select(PlayerDto.FromEntity),
        Rounds = game.Rounds.Select(RoundDto.FromEntity),
    };
}
    