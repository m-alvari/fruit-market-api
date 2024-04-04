
using fruit_market_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Db
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<UserRefreshToken> RefreshTokens { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("basket");
                entity.HasKey(x =>new { x.UserId ,   x.ProductId});

                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.ProductId).IsRequired();
                entity.Property(x => x.Count).IsRequired();
                entity.Property(x => x.DateCreation).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.FirstName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.LastName)
                   .HasMaxLength(150)
                   .IsRequired();

                entity.Property(x => x.Birthday)
                      .IsRequired();

                entity.Property(x => x.Email)
                     .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Gender)
                  .IsRequired();

                entity.Property(x => x.PhoneNumber)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(x => x.Password)
                     .HasMaxLength(150)
                       .IsRequired();

                entity.Property(x => x.ImageProfile)
                        .HasColumnType("TEXT")
                        .IsRequired();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable("userRefreshToken");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserId)
                    .IsRequired();

                entity.Property(x => x.Id)
                     .IsRequired()
                     .ValueGeneratedOnAdd();

                entity.Property(x => x.RefreshToken)
                   .IsRequired();

                entity.Property(x => x.IsActive)
                    .IsRequired();
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}