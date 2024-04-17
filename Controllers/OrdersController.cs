
using fruit_market_api.Db;
using fruit_market_api.Models;
using fruit_market_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrdersController : ControllerBase
  {
    private readonly ShopContext _context;
    private readonly ICurrentUserService _currentUserService;

    public OrdersController(ShopContext context, ICurrentUserService currentUserService)
    {
      _context = context;
      _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<OrderDetail[]> GetAll()
    {
      var qOrder = from o in _context.Orders
                   where o.UserId == _currentUserService.User.UserId
                   join p in _context.OrderDetail on o.Id equals p.ProductId
                   select new OrderDetail
                   {
                     Count = p.Count,
                     Price = p.Price,
                     OrderId = p.OrderId,
                     ProductId = p.ProductId
                   };

      qOrder = qOrder.OrderByDescending(c => c.Price);


      var order = await qOrder.ToArrayAsync();
      return order;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrder req)
    {
      var qOrder = from p in _context.Products
                   join r in req.ProductIds on p.Id equals r
                   join b in _context.Baskets on p.Id equals b.ProductId
                   where b.UserId == _currentUserService.User.UserId
                   select new { p, b };

      var products = await qOrder.ToArrayAsync();

      var price = products.Select(x => x.p.Price * x.b.Count).Sum();
      var order = new Order
      {
        DataCreation = DateTime.Now,
        Price = price,
        UserId = _currentUserService.User.UserId
      };

      await _context.Orders.AddAsync(order);
      await _context.SaveChangesAsync();
      foreach (var item in products)
      {
        var orderDetail = new OrderDetail
        {
          Count = item.b.Count,
          OrderId = order.Id,
          Price = item.p.Price,
          ProductId = item.p.Id

        };

        _context.OrderDetail.Add(orderDetail);

      }
      await _context.SaveChangesAsync();
      foreach (var item in products)
      {
        _context.Baskets.Remove(item.b);

      }
      await _context.SaveChangesAsync();
      return Ok(order);
    }
  }
}