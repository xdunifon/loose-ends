using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Players;

public record PlayerDto(string Id, string Name, int Points)
{
    public static PlayerDto FromEntity(Player player)
    {
        return new(player.PlayerId, player.Name, player.Responses.Sum(r => r.Votes.Count));
    }
}
