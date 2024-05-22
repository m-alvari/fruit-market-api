using FruitMarket.Domain.Models.Baskets;
using FruitMarket.Domain.Models.Favorites;
using FruitMarket.Domain.Models.Orders;
using FruitMarket.Domain.Models.Products;
using FruitMarket.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Modules;

public class FruitMarketDbContext(DbContextOptions<FruitMarketDbContext> options) : DbContext(options)
{

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FruitMarketDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

