using FruitMarket.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetail");
        builder.HasKey(m => new { m.OrderId, m.ProductId });
        builder.Property(m => m.Count).IsRequired();
        builder.Property(m => m.Price).IsRequired();
    }
}
