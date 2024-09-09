using GameStore.Api.Dtos;
using GameStore.Api.Dtos.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository(
    GameStoreDbContext dbContext, 
    ILogger<EntityFrameworkGamesRepository> logger
    ) : IGamesRepository
{
    private readonly GameStoreDbContext dbContext = dbContext;
    private readonly ILogger<EntityFrameworkGamesRepository> logger = logger;
    public async  Task<IEnumerable<GameEntity>> GetAllAsync()
    {
        throw new InvalidOperationException("The DB connection is closed!");
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task<GameEntity?> GetAsync(int id)
    {
        throw new InvalidOperationException("The DB connection is closed!");
        return await dbContext.Games.FindAsync(id);
    }


    public async Task CreateAsync(CreateGameDto createGameDto)
    {
        dbContext.Games.Add(new GameEntity()
        {
            Name = createGameDto.Name,
            Genre = createGameDto.Genre,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate,
            ImageUri = createGameDto.ImageUri
        });
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Created game {Name} with price {Price}.", createGameDto.Name, createGameDto.Price);
    }

    public async  Task UpdateAsync(int id, UpdateGameDto updateGameDto)
    {
        GameEntity? game = await GetAsync(id);
        
        if (game is null) throw new KeyNotFoundException("Game not found");
        
        game.Name = updateGameDto.Name;
        game.Genre = updateGameDto.Genre;
        game.Price = updateGameDto.Price;
        game.ReleaseDate = updateGameDto.ReleaseDate;
        game.ImageUri = updateGameDto.ImageUri;

        dbContext.Games.Update(game);
        await dbContext.SaveChangesAsync();
    }

    public async Task  DeleteAsync(int id) =>  await dbContext.Games
        .Where(game => game.Id == id)
        .ExecuteDeleteAsync();
}