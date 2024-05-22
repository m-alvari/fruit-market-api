using FruitMarket.Domain.Models.Orders;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Order>> Get()
    {
        return Ok(await service.GetOrderAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Post([FromBody] int[] productsId)
    {
        return Ok(await service.CreateOrder(productsId));
    }

}
