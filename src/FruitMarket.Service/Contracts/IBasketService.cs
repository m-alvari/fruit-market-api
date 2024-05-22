using FruitMarket.Domain.Models.Baskets;

namespace FruitMarket.Service.Contracts;

public interface IBasketService
{
    Task<Basket?> GetBasketWithId(int productId);
    Task<BasketDetailDto[]> GetBasketsAsync();
    Task<BasketDto?> AddOrUpdateBasketAsync(UpsertBasketDto dto);
    Task DeleteBasketAsync(int productId);
}
