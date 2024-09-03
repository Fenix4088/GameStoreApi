using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Dtos.Data.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<GameEntity>
{
    
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.Property(game => game.Price)
            .HasPrecision(5,2);
    }
}