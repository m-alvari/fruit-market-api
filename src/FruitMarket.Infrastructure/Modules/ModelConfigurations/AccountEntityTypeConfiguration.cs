using FruitMarket.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitMarket.Infrastructure.Modules.ModelConfigurations;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId)
        .IsRequired().ValueGeneratedNever();
        builder.Property(x => x.Password)
        .IsRequired();
    }
}
