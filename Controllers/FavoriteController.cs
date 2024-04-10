
using fruit_market_api.Db;
using fruit_market_api.Models;
using fruit_market_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruit_market_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly ShopContext _context;
        private readonly ICurrentUserService _currentUserService;

        public FavoriteController(ShopContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        [HttpGet]
        public async Task<FavoriteDetail[]> GetAll()
        {
            var query = from f in _context.Favorites
                        where f.UserId == _currentUserService.User.UserId && f.IsDelete == false
                        join p in _context.Products on f.ProductId equals p.Id
                        select new FavoriteDetail
                        {
                            Price = p.Price,
                            ProductId = f.ProductId,
                            Name = p.Name
                        };


            query = query.OrderByDescending(c => c.Price);


            var favorite = await query.ToArrayAsync();
            return favorite;
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> DeleteFavorite(int productId)
        {
            var favorite = await _context.Favorites
            .FirstOrDefaultAsync(x => x.UserId == _currentUserService.User.UserId && x.ProductId == productId);

            if (favorite == null)
            {
                return NotFound();
            }
            favorite.IsDelete = true;
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Upsert(UpsertFavoriteRequest req)
        {

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(x => x.UserId == _currentUserService.User.UserId && x.ProductId == req.ProductId);

            if (favorite == null)
            {
                favorite = new Favorite()
                {
                    ProductId = req.ProductId,


                    UserId = _currentUserService.User.UserId
                };
                await _context.AddAsync(favorite);
            }
            else
            {
                favorite.IsDelete = !favorite.IsDelete;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }


    }

}
