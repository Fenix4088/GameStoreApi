using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    IEnumerable<GameEntity> GetAll();
    GameEntity? Get(int id);
    void Create(CreateGameDto createGameDto);
    void Update(int id, UpdateGameDto updateGameDto);
    void Delete(int id);
}