using FruitMarket.Domain.Models.Orders;

namespace FruitMarket.Domain.Contracts;

public interface IOrderRepository
{
    Task<OrderWithDetail[]> GetAsync(int UserId);
    Task<Order> AddAsync(Order order);
    Task<OrderDetail> AddOrderDetailAsync(OrderDetail order);


}
