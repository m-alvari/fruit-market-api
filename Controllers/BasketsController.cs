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
        public async Task<BasketDetail[]> GetAll()
        {
            var query = from b in _context.Baskets
                        where b.UserId == UserId
                        join p in _context.Products on b.ProductId equals p.Id
                        select new BasketDetail {
                            Count=b.Count,
                            Price = p.Price,
                            DateCreation = b.DateCreation,
                            ProductId = b.ProductId,
                            Name = p.Name
                        };


            query = query.OrderByDescending(c => c.DateCreation);


            var baskets = await query.ToArrayAsync();
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
        public async Task<ActionResult> DeleteBasket(int productId)
        {
            var basket = await _context.Baskets
            .FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == productId);

            if (basket == null)
            {
                return NotFound();
            }
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<UpsertBasketRequest>> GetBasket(int productId)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == productId);
            if (basket == null)
            {
                return new UpsertBasketRequest { Count = 0, ProductId = productId };
            }
            return Ok(basket);
        }
    }
}