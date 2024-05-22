using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Orders;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Repositories;

public class OrderRepository(FruitMarketDbContext db) : IOrderRepository
{

    public async Task<Order> AddAsync(Order order)
    {
        db.Orders.Add(order);
        await db.SaveChangesAsync();
        return order;
    }
       public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail order)
    {
        db.OrderDetails.Add(order);
        await db.SaveChangesAsync();
        return order;
    }

    public async Task<OrderWithDetail[]> GetAsync(int UserId)
    {
        var qOrder = from o in db.Orders
                     where o.UserId == UserId
                     let d = db.OrderDetails.Where(x => x.OrderId == o.Id).ToArray()
                     select new OrderWithDetail
                     {
                         DataCreation = o.DataCreation,
                         Price = o.Price,

                         Details = d.Select(p => new OrderDetailDto
                         {
                             Count = p.Count,
                             Price = p.Price,
                             OrderId = p.OrderId,
                             ProductId = p.ProductId
                         }).ToArray()
                     };
        qOrder = qOrder.OrderByDescending(o => o.DataCreation);

        var order = await qOrder.ToArrayAsync();
        return order;
    }



}
