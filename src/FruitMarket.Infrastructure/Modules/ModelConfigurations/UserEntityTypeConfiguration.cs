using FruitMarket.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
        .ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName)
        .IsRequired()
        .HasMaxLength(150);
        builder.Property(x => x.LastName)
        .IsRequired()
        .HasMaxLength(150);
        builder.Property(x => x.Email)
        .IsRequired()
        .HasMaxLength(150);
        builder.Property(x => x.Birthday)
        .IsRequired();
        builder.Property(x => x.PhoneNumber)
        .IsRequired()
        .HasMaxLength(150);
        builder.Property(x => x.Gender)
        .IsRequired();
        builder.Property(x => x.ImageProfile);
        builder.Property(x => x.Address)
        .HasMaxLength(200);
    }
}

