
using GameStore.Api.Entities;
using GameStore.Api.Models;

const string GetGamesEndpointName = "GetGame";

List <Game> games = new()
{
    new Game()
    {
        Id = 1,
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateTime(1991, 2, 1),
        ImageUri = "https://placehold.com/100"
    },
    new Game()
    {
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaynig",
        Price = 19.99M,
        ReleaseDate = new DateTime(2010, 9, 30),
        ImageUri = "https://placehold.com/100"
    },
    new Game()
    {
        Id = 3,
        Name = "FIFA 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateTime(2022, 9, 27),
        ImageUri = "https://placehold.com/100"
    },
};



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var group = app.MapGroup("/games")
    .WithParameterValidation();

group.MapGet("", () => games)
    .WithName("GetGames")
    .WithOpenApi();

group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null) return Results.NotFound();
    
    return Results.Ok(game);

}).WithName(GetGamesEndpointName);

group.MapPost("", (CreateGamePayload payload) =>
{
    var newGame = new Game()
    {
        Id = new Random().Next(),
        Name = payload.Name,
        Genre = payload.Genre,
        Price = payload.Price,
        ReleaseDate = payload.ReleaseDate,
        ImageUri = payload.ImageUri
    };
    games.Add(newGame);

    return Results.CreatedAtRoute(GetGamesEndpointName, new { id = newGame.Id }, newGame);
}).WithName("CreateNewGame");


group.MapPut("/{id}", (int id, CreateGamePayload payload) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game is null) return Results.NotFound();
    
    games.Select(game =>
    {
        if (game.Id == id)
        {
            game.Name = payload.Name;
            game.Genre = payload.Genre;
            game.Price = payload.Price;
            game.ReleaseDate = payload.ReleaseDate;
            game.ImageUri = payload.ImageUri;
        };

        return game;
    }).ToArray();

    return Results.Ok(game);

}).WithName("UpdateGame");

group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);
    if (game is null) return Results.NotFound();

    games.Remove(game);

    return Results.NoContent();

}).WithName("DeleteGame");

app.Run();