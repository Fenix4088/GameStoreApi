using System.Reflection;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Dtos.Data;

public class GameStoreDbContext: DbContext
{
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
    : base(options)
    {
        
    }

    public DbSet<GameEntity> Games => Set<GameEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}