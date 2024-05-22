using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Baskets;
using FruitMarket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FruitMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IBasketService services) : ControllerBase
    {

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Basket>> GetWithId(int productId)
        {
            return Ok(await services.GetBasketWithId(productId));
        }


        [HttpPost]
        public async Task<ActionResult<BasketDto>> Post([FromBody] UpsertBasketDto basket)
        {
            return Ok(await services.AddOrUpdateBasketAsync(basket));
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            await services.DeleteBasketAsync(productId);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<BasketDetailDto[]>> Get()
        {
            return Ok(await services.GetBasketsAsync());
        }
    }
}
