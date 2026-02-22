using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Players;

public record PlayerDto(int Id, string Name, int Points)
{
    public static PlayerDto FromEntity(Player player)
    {
        return new(player.Id, player.Name, player.Responses.Sum(r => r.Votes.Count));
    }
}
