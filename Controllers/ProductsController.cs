using fruit_market_api.Db;
using fruit_market_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<Product[]> GetAll(
            [FromQuery] string? q = null,
            [FromQuery] int take = 8,
            [FromQuery] int skip = 0,
            [FromQuery] OrderBy orderBy = OrderBy.Asc
            )
        {
            var query = _context.Products.AsQueryable();

            if (q != null && q != string.Empty)
            {
                query = query.Where(x => x.Name.Contains(q));
            }

            query = orderBy == OrderBy.Desc 
            ? query.OrderByDescending(c => c.Name) 
            : query.OrderBy(c => c.Name);

            var products = await query.Skip(skip).Take(take).ToArrayAsync();
            return products;
        }
     
        [HttpPost]
        public async Task<Product> CreateProduct(UpsertProductRequest req)
        {
            var product = new Product()
            {
                Name = req.Name,
                Price = req.Price,
                ImageUrl = req.ImageUrl
            };
            await _context.AddAsync(product);

            await _context.SaveChangesAsync();
            return product;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult<Product>> UpdateProduct(int id, UpsertProductRequest req)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = req.Name;
            product.Price = req.Price;
            product.ImageUrl = product.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok(product);

        }

    }
}