using FruitMarket.Domain.Models.Orders;

namespace FruitMarket.Service.Contracts;

public interface IOrderService
{
    Task<Order> CreateOrder(int[] productIds);
    Task<OrderWithDetail[]> GetOrderAsync();
}
