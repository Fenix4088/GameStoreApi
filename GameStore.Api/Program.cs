using GameStore.Api.Dtos.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGamesEndpoints();

app.Run();