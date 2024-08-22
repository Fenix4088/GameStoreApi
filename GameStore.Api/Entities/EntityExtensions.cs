using GameStore.Api.Dtos;

namespace GameStore.Api.Entities;

public static class EntityExtensions
{
    public static GameDto AsDto(this GameEntity gameEntity)
    {
        return new GameDto(
            gameEntity.Id,
            gameEntity.Name,
            gameEntity.Genre,
            gameEntity.Price,
            gameEntity.ReleaseDate,
            gameEntity.ImageUri
        );
    }
}