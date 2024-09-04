using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Dtos.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceProvider, IConfiguration configration)
    {
        var connectionString = configration.GetConnectionString("GameStoreDbContext");
        
        serviceProvider
            .AddSqlServer<GameStoreDbContext>(connectionString)
            .AddScoped<IGamesRepository, EntityFrameworkGamesRepository>()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return serviceProvider;
    }
}