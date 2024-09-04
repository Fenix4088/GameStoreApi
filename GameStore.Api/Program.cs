using GameStore.Api.Authorization;
using GameStore.Api.Dtos.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();

var app = builder.Build();

await app.Services.InitializeDbAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGamesEndpoints();

app.Run();