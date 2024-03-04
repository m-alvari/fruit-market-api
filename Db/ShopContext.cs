using ConsoleApp.PostgreSQL.Models;
using fruit_market_api.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.PostgreSQL
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

            base.OnModelCreating(modelBuilder);
        }

    }
}