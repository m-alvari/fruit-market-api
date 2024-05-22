using FruitMarket.Domain.Models.Baskets;

namespace FruitMarket.Domain.Contracts;

public interface IBasketRepository
{
    Task<BasketDetailDto[]> Get(int userId);
    Task<Basket?> FindAsync(int userId, int productId);
    Task<Basket> AddAsync(Basket basket);
    Task<int> DeleteAsync(Basket basket);
    Task<Basket> UpdateAsync(Basket basket);
    Task<Basket[]> GetUserBasket(int userId, int[] productIds);
}
