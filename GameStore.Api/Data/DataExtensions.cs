using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Dtos.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreDbContext>();
        dbContext.Database.Migrate();
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