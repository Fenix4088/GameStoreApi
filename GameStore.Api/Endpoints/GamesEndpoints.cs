using GameStore.Api.Entities;
using GameStore.Api.Models;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    
    private const string GetGamesEndpointName = "GetGame";
    
 

   public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
   {
       InMemGamesRepository inMemGamesRepository = new();
      
      var group = routes.MapGroup("/games")
          .WithParameterValidation();
      
      group.MapGet("", () => inMemGamesRepository.GetAll())
          .WithName("GetGames")
          .WithOpenApi();
      
      group.MapGet("/{id}", (int id) =>
      {
          var game = inMemGamesRepository.Get(id);

          if (game is null) return Results.NotFound();

          return Results.Ok(game);
      }).WithName(GetGamesEndpointName);
      
      group.MapPost("", (CreateGamePayload payload) => inMemGamesRepository.Create(payload)).WithName("CreateNewGame");
      
      
      group.MapPut("/{id}", (int id, CreateGamePayload payload) =>
      {
          try
          {
              inMemGamesRepository.Update(id, payload);

              return Results.Ok();
          }
          catch (Exception e)
          {
              return Results.NotFound(e.Message);
          }
      }).WithName("UpdateGame");
      
      group.MapDelete("/{id}", (int id) => inMemGamesRepository.Delete(id)).WithName("DeleteGame");

      return group;
   }
}