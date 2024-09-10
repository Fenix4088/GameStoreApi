using GameStore.Api.Authorization;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    
    private const string GetGamesV1EndpointName = "GetGameV1";
    private const string GetGamesV2EndpointName = "GetGameV2";
    
   public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
   {
      
      var group = routes.NewVersionedApi()
          .MapGroup("/games")
          .HasApiVersion(1.0)
          .HasApiVersion(2.0)
          .WithParameterValidation();      
      
      
      group.MapGet("", async (IGamesRepository gamesRepository, ILoggerFactory loggerFactory) =>
          {
                var allGames = await gamesRepository.GetAllAsync();
                return Results.Ok(allGames.Select(gameEntity => gameEntity.AsDtoV1()));
          })
          .WithName("GetGames")
          .WithOpenApi()
          .MapToApiVersion(1.0);
      
      group.MapGet("/{id}", async (IGamesRepository gamesRepository, int id) =>
      {
          var game = await gamesRepository.GetAsync(id);

          if (game is null) return Results.NotFound();

          return Results.Ok(game.AsDtoV1());
      })
          .WithName(GetGamesV2EndpointName)
          .RequireAuthorization(Policies.ReadAccess)
          .MapToApiVersion(1.0);
      
      group.MapGet("", async (IGamesRepository gamesRepository, ILoggerFactory loggerFactory) =>
          {
              var allGames = await gamesRepository.GetAllAsync();
              return Results.Ok(allGames.Select(gameEntity => gameEntity.AsDtoV2()));
          })
          .WithName("GetGamesV2")
          .WithOpenApi()
          .MapToApiVersion(2.0);
      
      group.MapGet("/{id}", async (IGamesRepository gamesRepository, int id) =>
          {
              var game = await gamesRepository.GetAsync(id);

              if (game is null) return Results.NotFound();

              return Results.Ok(game.AsDtoV2());
          })
          .WithName(GetGamesV1EndpointName)
          .RequireAuthorization(Policies.ReadAccess)
          .MapToApiVersion(2.0);
      
      group.MapPost("", (IGamesRepository gamesRepository, CreateGameDto createGameDto) => gamesRepository.CreateAsync(createGameDto))
          .WithName("CreateNewGame")
          .RequireAuthorization(Policies.WriteAccess)
          .MapToApiVersion(1.0);
      
      
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
          .RequireAuthorization(Policies.WriteAccess)
          .MapToApiVersion(1.0);
      
      group.MapDelete("/{id}", (IGamesRepository gamesRepository, int id) => gamesRepository.DeleteAsync(id))
          .WithName("DeleteGame")
          .RequireAuthorization(Policies.WriteAccess)
          .MapToApiVersion(1.0);

      return group;
   }
}