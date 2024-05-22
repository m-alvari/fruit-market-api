using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Orders;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class OrderService(IOrderRepository repo,
    ICurrentUserService CurrentUserService,
    IBasketRepository basketRepository,
    IProductRepository productRepository
) : IOrderService
{

    public async Task<OrderWithDetail[]> GetOrderAsync()
    {
        var order = await repo.GetAsync(CurrentUserService.User.UserId);
        return order;
    }

    public async Task<Order> CreateOrder(int[] productIds)
    {
        var baskets = await basketRepository.GetUserBasket(CurrentUserService.User.UserId, productIds);
        var products = await productRepository.GetUserProduct(productIds);

        var pro = from b in baskets
                  join p in products
                  on b.ProductId equals p.Id
                  select new { p, b };

        var sum = pro.Select(x => x.p.Price * x.b.Count).Sum();
        var order = new Order
        {
            DataCreation = DateTime.Now,
            Price = sum,
            UserId = CurrentUserService.User.UserId
        };

        await repo.AddAsync(order);
        foreach (var item in pro)
        {
            var orderDetail = new OrderDetail
            {
                Count = item.b.Count,
                OrderId = order.Id,
                Price = item.p.Price,
                ProductId = item.p.Id

            };

            await repo.AddOrderDetailAsync(orderDetail);

        }

        foreach (var item in pro)
        {
            await basketRepository.DeleteAsync(item.b);

        }
        return order;
    }
}

