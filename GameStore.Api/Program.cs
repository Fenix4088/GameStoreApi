using System.Diagnostics;
using GameStore.Api.Authorization;
using GameStore.Api.Dtos.Data;
using GameStore.Api.Dtos.Middleware;
using GameStore.Api.Endpoints;
using GameStore.Api.ErrorHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o => { });

builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddGameStoreAuthorization();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.ConfigureExceptionsHandler());
app.UseMiddleware<RequestTimingMiddleware>();

await app.Services.InitializeDbAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpLogging();
app.MapGamesEndpoints();

app.Run();