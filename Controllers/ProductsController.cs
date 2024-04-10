using fruit_market_api.Db;
using fruit_market_api.Models;
using fruit_market_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;



namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ProductsController(ShopContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ProductDetail[]> GetAll(
            [FromQuery] string? q = null,
            [FromQuery] int take = 8,
            [FromQuery] int skip = 0,
            [FromQuery] OrderBy orderBy = OrderBy.Asc
            )
        {
            var isLogin = _currentUserService.IsLogin();

            var query = _context.Products.AsQueryable();

            if (q != null && q != string.Empty)
            {
                query = query.Where(x => x.Name.Contains(q));
            }

            query = orderBy == OrderBy.Desc
            ? query.OrderByDescending(c => c.Name)
            : query.OrderBy(c => c.Name);

            var pQuery = query.Skip(skip).Take(take);
            if (isLogin)
            {

                var nQuery = from p in pQuery
                             join f in _context.Favorites.Where(x => x.UserId == _currentUserService.User.UserId && x.IsDelete == false) on p.Id equals f.ProductId into qq
                             from qz in qq.DefaultIfEmpty()
                             select new ProductDetail
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Price = p.Price,
                                 ImageUrl = p.ImageUrl,
                                 IsFavorite = qz != null
                             };
                return await nQuery.ToArrayAsync();
            }
            else
            {
                var nQuery = from p in pQuery

                             select new ProductDetail
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Price = p.Price,
                                 ImageUrl = p.ImageUrl,
                                 IsFavorite = false
                             };
                return await nQuery.ToArrayAsync();
            }


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

        [HttpGet("images/{id:int}")]
        public async Task<ActionResult> GetProductImage(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            // Extract content type and Base64 data
            string[] parts = product.ImageUrl.Split(',');
            if (parts.Length != 2)
            {
                // Handle invalid input
                return BadRequest("Invalid Base64 string format.");
            }

            string contentType = parts[0].Replace("data:", "").Replace(";base64", ""); // "data:image/jpeg;base64"

            string base64Data = parts[1];

            // Convert Base64 string to byte array
            byte[] imageBytes = Convert.FromBase64String(base64Data);

            return File(imageBytes, contentType); // Content type extracted from Base64 string
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