using GameStore.Api.Dtos;
using GameStore.Api.Dtos.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository(GameStoreDbContext dbContext) : IGamesRepository
{
    private readonly GameStoreDbContext dbContext = dbContext;
    public IEnumerable<GameEntity> GetAll() => dbContext.Games.AsNoTracking().ToList();

    public GameEntity? Get(int id) => dbContext.Games.Find(id);

    public void Create(CreateGameDto createGameDto)
    {
        dbContext.Games.Add(new GameEntity()
        {
            Name = createGameDto.Name,
            Genre = createGameDto.Genre,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate,
            ImageUri = createGameDto.ImageUri
        });
        dbContext.SaveChanges();
    }

    public void Update(int id, UpdateGameDto updateGameDto)
    {
        GameEntity? game = Get(id);
        
        if (game is null) throw new KeyNotFoundException("Game not found");
        
        game.Name = updateGameDto.Name;
        game.Genre = updateGameDto.Genre;
        game.Price = updateGameDto.Price;
        game.ReleaseDate = updateGameDto.ReleaseDate;
        game.ImageUri = updateGameDto.ImageUri;

        dbContext.Games.Update(game);
        dbContext.SaveChanges();
    }

    public void Delete(int id) => dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
}