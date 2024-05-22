using FruitMarket.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.ToTable("Product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(b => b.Name)
            .HasMaxLength(100)
            .IsUnicode()
            .IsRequired();

        builder
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Product_Name");

    }
}

