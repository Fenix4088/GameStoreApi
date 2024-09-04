using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    Task<IEnumerable<GameEntity>> GetAllAsync();
    Task<GameEntity?> GetAsync(int id);
    Task CreateAsync(CreateGameDto createGameDto);
    Task UpdateAsync(int id, UpdateGameDto updateGameDto);
    Task DeleteAsync(int id);
}