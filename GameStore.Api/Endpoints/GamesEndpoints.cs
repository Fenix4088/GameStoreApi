using GameStore.Api.Authorization;
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
      
      group.MapGet("", async (IGamesRepository gamesRepository, ILoggerFactory loggerFactory) =>
          {
                var allGames = await gamesRepository.GetAllAsync();
                return Results.Ok(allGames.Select(gameEntity => gameEntity.AsDto()));
          })
          .WithName("GetGames")
          .WithOpenApi();
      
      group.MapGet("/{id}", async (IGamesRepository gamesRepository, int id) =>
      {
          var game = await gamesRepository.GetAsync(id);

          if (game is null) return Results.NotFound();

          return Results.Ok(game.AsDto());
      })
          .WithName(GetGamesEndpointName)
          .RequireAuthorization(Policies.ReadAccess);
      
      group.MapPost("", (IGamesRepository gamesRepository, CreateGameDto createGameDto) => gamesRepository.CreateAsync(createGameDto))
          .WithName("CreateNewGame")
          .RequireAuthorization(Policies.WriteAccess);
      
      
      
      group.MapPut("/{id}", (IGamesRepository gamesRepository, int id, UpdateGameDto updateGameDto) =>
      {
          try
          {
              gamesRepository.UpdateAsync(id, updateGameDto);

              return Results.Ok();
          }
          catch (Exception e)
          {
              return Results.NotFound(e.Message);
          }
      })
          .WithName("UpdateGame")
          .RequireAuthorization(Policies.WriteAccess);
      
      group.MapDelete("/{id}", (IGamesRepository gamesRepository, int id) => gamesRepository.DeleteAsync(id))
          .WithName("DeleteGame")
          .RequireAuthorization(Policies.WriteAccess);

      return group;
   }
}