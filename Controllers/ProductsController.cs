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
        public async Task<Product[]> GetAll()
        {
            var products = await _context.Products.ToArrayAsync();
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
    }
}