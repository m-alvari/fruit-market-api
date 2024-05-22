using FruitMarket.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id)
        .IsRequired()
        .ValueGeneratedOnAdd();

        builder.Property(n => n.UserId).IsRequired();

        builder.Property(n => n.Price).IsRequired();

        builder.Property(n => n.DataCreation).IsRequired();
    }
}
