using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Baskets;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class BasketService(IBasketRepository repo, ICurrentUserService currentUserService) : IBasketService
{


    public async Task<Basket?> GetBasketWithId(int productId)
    {
        var basket = await repo.FindAsync(currentUserService.User.UserId, productId);
        if (basket != null)
        {
            return basket;
        }
        return null;

    }


    public async Task<BasketDetailDto[]> GetBasketsAsync()
    {
        var list = await repo.Get(currentUserService.User.UserId);
        return list;
    }

    public async Task<BasketDto?> AddOrUpdateBasketAsync(UpsertBasketDto dto)
    {
        var basket = await repo.FindAsync(currentUserService.User.UserId, dto.ProductId);

        if (basket == null)
        {
            basket = new Basket
            {
                UserId = currentUserService.User.UserId,
                ProductId = dto.ProductId,
                Count = dto.Count,
                DateCreation = DateTime.Now
            };
            await repo.AddAsync(basket);
        }
        else
        {
            if (dto.Count == 0)
            {
                await repo.DeleteAsync(basket);
            }
            else
            {
                basket.Count = dto.Count;
                await repo.UpdateAsync(basket);
            }
        }

        if (basket.Count == 0)
        {
            return null;
        }
        var newBasket = new BasketDto
        {
            ProductId = basket.ProductId,
            Count = basket.Count,
            DateCreation = basket.DateCreation
        };
        return newBasket;

    }

    public async Task DeleteBasketAsync(int ProductId)
    {
        var entity = await repo.FindAsync(currentUserService.User.UserId, ProductId);
        if (entity != null)
        {
            await repo.DeleteAsync(entity);
        }
    }

}
