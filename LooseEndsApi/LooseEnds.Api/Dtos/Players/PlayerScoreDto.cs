using LooseEnds.Database.Entities;

namespace LooseEnds.Api.Dtos.Players;

public record PlayerScoreDto(string PlayerId, string Name, int Score)
{
    public static PlayerScoreDto FromEntity(Player entity)
    {
        return new(entity.PlayerId, entity.Name, entity.Responses.Sum(r => r.Votes.Count));
    }
}