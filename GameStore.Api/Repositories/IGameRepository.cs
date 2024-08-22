using GameStore.Api.Entities;
using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public interface IGamesRepository
{
    IEnumerable<GameEntity> GetAll();
    GameEntity? Get(int id);
    void Create(CreateGamePayload payload);
    void Update(int id, CreateGamePayload payload);
    void Delete(int id);
}