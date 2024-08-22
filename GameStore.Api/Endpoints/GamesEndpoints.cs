using GameStore.Api.Entities;
using GameStore.Api.Models;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    
    private const string GetGamesEndpointName = "GetGame";
    
   public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
   {
      
      var group = routes.MapGroup("/games")
          .WithParameterValidation();
      
      group.MapGet("", (IGamesRepository gamesRepository) => gamesRepository.GetAll())
          .WithName("GetGames")
          .WithOpenApi();
      
      group.MapGet("/{id}", (IGamesRepository gamesRepository, int id) =>
      {
          var game = gamesRepository.Get(id);

          if (game is null) return Results.NotFound();

          return Results.Ok(game);
      }).WithName(GetGamesEndpointName);
      
      group.MapPost("", (IGamesRepository gamesRepository, CreateGamePayload payload) => gamesRepository.Create(payload)).WithName("CreateNewGame");
      
      
      group.MapPut("/{id}", (IGamesRepository gamesRepository, int id, CreateGamePayload payload) =>
      {
          try
          {
              gamesRepository.Update(id, payload);

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