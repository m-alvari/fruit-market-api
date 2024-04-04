using fruit_market_api.Db;
using fruit_market_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly int UserId = 20;
        private readonly ShopContext _context;

        public BasketsController(ShopContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<Basket[]> GetAll()
        {
            var query = _context.Baskets.AsQueryable();


            query = query.OrderByDescending(c => c.DateCreation);


            var baskets = await query.Where(x=>x.UserId == UserId).ToArrayAsync();
            return baskets;
        }

        [HttpPost]
        public async Task<Basket> UpsertBasket(UpsertBasketRequest req)
        {

            var basket = await _context.Baskets
                .FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == req.ProductId);

            if (basket == null)
            {
                basket = new Basket()
                {
                    ProductId = req.ProductId,
                    Count = req.Count,
                    DateCreation = DateTime.Now,
                    UserId = UserId
                };
                await _context.AddAsync(basket);
            }
            else
            {
                if (req.Count == 0)
                {
                    _context.Remove(basket);
                }
                else
                {
                    basket.Count = req.Count;
                }
            }

            await _context.SaveChangesAsync();
            return basket;
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> DeleteBasket( int productId)
        {
            var basket = await _context.Baskets
            .FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == productId);

            if (basket == null )
            {
                return NotFound();
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}