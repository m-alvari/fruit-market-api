using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Baskets;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;


namespace FruitMarket.Infrastructure.Repositories;

public class BasketRepository(FruitMarketDbContext db) : IBasketRepository
{

    public async Task<Basket[]> GetUserBasket(int userId, int[] productIds)
    {
        var q = (from b in db.Baskets
                 where b.UserId == userId && productIds.Contains(b.ProductId)
                 select b).ToArrayAsync();
                 return await q;
    }

    public async Task<BasketDetailDto[]> Get(int userId)
    {
        var q = from b in db.Baskets
                where b.UserId == userId
                join p in db.Products on b.ProductId equals p.Id
                select new BasketDetailDto
                {
                    Count = b.Count,
                    DateCreation = b.DateCreation,
                    Price = p.Price,
                    Name = p.Name,
                    ProductId = b.ProductId,
                    ImageUrl = p.ImageUrl,
                };
        q = q.OrderByDescending(v => v.DateCreation);
        var basket = await q.ToArrayAsync();
        return basket;
    }

    public Task<Basket?> FindAsync(int userId, int productId)
    {
        return db.Baskets.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
    }

    public async Task<Basket> AddAsync(Basket basket)
    {
        db.Baskets.Add(basket);
        await db.SaveChangesAsync();
        return basket;
    }

    public Task<int> DeleteAsync(Basket basket)
    {
        db.Baskets.Remove(basket);
        return db.SaveChangesAsync();
    }

    public async Task<Basket> UpdateAsync(Basket basket)
    {
        db.Baskets.Update(basket);
        await db.SaveChangesAsync();
        return basket;
    }
}

