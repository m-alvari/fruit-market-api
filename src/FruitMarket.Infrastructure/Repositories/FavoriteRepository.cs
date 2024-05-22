using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Favorites;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Repositories;

public class FavoriteRepository(FruitMarketDbContext db) : IFavoriteRepository
{
    public async Task<FavoriteDetailDto[]> GetAsync(int userId)
    {
        var q = from f in db.Favorites
                where f.UserId == userId && f.IsDelete == false
                join p in db.Products on f.ProductId equals p.Id
                select new FavoriteDetailDto
                {
                    Price = p.Price,
                    Name = p.Name,
                    ProductId = p.Id,
                    imageUrl = p.ImageUrl,
                };
        q = q.OrderByDescending(f => f.Price);
        var favorite = await q.ToArrayAsync();
        return favorite;
    }

    public async Task<Favorite> AddAsync(Favorite favorite)
    {
        db.Favorites.Add(favorite);
        await db.SaveChangesAsync();
        return favorite;
    }

    public Task<Favorite?> FindAsync(int userId, int productId)
    {
        return db.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public Task<int> DeleteAsync(Favorite favorite)
    {
        db.Favorites.Remove(favorite);
        return db.SaveChangesAsync();
    }

    public async Task<Favorite?> UpdateAsync(Favorite favorite)
    {
        db.Favorites.Update(favorite);
        await db.SaveChangesAsync();
        return favorite;
    }
}
