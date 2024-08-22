using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Models;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
    private static List <GameEntity> Games = new()
    {
        new GameEntity()
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 2, 1),
            ImageUri = "https://placehold.com/100"
        },
        new GameEntity()
        {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaynig",
            Price = 19.99M,
            ReleaseDate = new DateTime(2010, 9, 30),
            ImageUri = "https://placehold.com/100"
        },
        new GameEntity()
        {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateTime(2022, 9, 27),
            ImageUri = "https://placehold.com/100"
        },
    };

    public IEnumerable<GameEntity> GetAll() => Games;

    public GameEntity? Get(int id) => Games.Find(game => game.Id == id);

    public void Create(CreateGameDto createGameDto)
    {
        var newGame = new GameEntity()
        {
            Id = new Random().Next(),
            Name = createGameDto.Name,
            Genre = createGameDto.Genre,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate,
            ImageUri = createGameDto.ImageUri
        };
        Games.Add(newGame);
    }

    public void Update(int id, UpdateGameDto updateGameDto)
    {
        
        GameEntity? game = Get(id);
        
        if (game is null) throw new KeyNotFoundException("Game not found");
          
        Games.Select(game =>
        {
            if (game.Id == id)
            {
                game.Name = updateGameDto.Name;
                game.Genre = updateGameDto.Genre;
                game.Price = updateGameDto.Price;
                game.ReleaseDate = updateGameDto.ReleaseDate;
                game.ImageUri = updateGameDto.ImageUri;
            };
      
            return game;
        }).ToArray();
    }

    public void Delete(int id) {
        GameEntity? game = Get(id);
        Games.Remove(game);
    }
}