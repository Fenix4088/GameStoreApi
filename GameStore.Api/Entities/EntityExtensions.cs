using GameStore.Api.Dtos;

namespace GameStore.Api.Entities;

public static class EntityExtensions
{
    public static GameDtoV1 AsDtoV1(this GameEntity gameEntity)
    {
        return new GameDtoV1(
            gameEntity.Id,
            gameEntity.Name,
            gameEntity.Genre,
            gameEntity.Price,
            gameEntity.ReleaseDate,
            gameEntity.ImageUri
        );
    }
    
    public static GameDtoV2 AsDtoV2(this GameEntity gameEntity)
    {
        return new GameDtoV2(
            gameEntity.Id,
            gameEntity.Name,
            gameEntity.Genre,
            gameEntity.Price - (gameEntity.Price * .3m),
            gameEntity.Price,
            gameEntity.ReleaseDate,
            gameEntity.ImageUri
        );
    }
}