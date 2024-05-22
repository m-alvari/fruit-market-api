using FruitMarket.Domain.Models.Baskets;
using FruitMarket.Domain.Models.Products;
using FruitMarket.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Basket");
        builder.HasKey(x => new { x.UserId, x.ProductId });
        builder.Property(x => x.UserId)
        .IsRequired();
        builder.Property(x => x.Count)
        .IsRequired();
        builder.Property(x => x.DateCreation)
        .IsRequired();
    }
}
