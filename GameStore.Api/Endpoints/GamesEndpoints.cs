using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    
    private const string GetGamesEndpointName = "GetGame";
    
   public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
   {
      
      var group = routes.MapGroup("/games")
          .WithParameterValidation();
      
      group.MapGet("", (IGamesRepository gamesRepository) => gamesRepository.GetAll().Select(gameEntity => gameEntity.AsDto()))
          .WithName("GetGames")
          .WithOpenApi();
      
      group.MapGet("/{id}", (IGamesRepository gamesRepository, int id) =>
      {
          var game = gamesRepository.Get(id);

          if (game is null) return Results.NotFound();

          return Results.Ok(game.AsDto());
      }).WithName(GetGamesEndpointName);
      
      group.MapPost("", (IGamesRepository gamesRepository, CreateGameDto createGameDto) => gamesRepository.Create(createGameDto)).WithName("CreateNewGame");
      
      
      group.MapPut("/{id}", (IGamesRepository gamesRepository, int id, UpdateGameDto updateGameDto) =>
      {
          try
          {
              gamesRepository.Update(id, updateGameDto);

              return Results.Ok();
          }
          catch (Exception e)
          {
              return Results.NotFound(e.Message);
          }
      }).WithName("UpdateGame");
      
      group.MapDelete("/{id}", (IGamesRepository gamesRepository, int id) => gamesRepository.Delete(id)).WithName("DeleteGame");

      return group;
   }
}