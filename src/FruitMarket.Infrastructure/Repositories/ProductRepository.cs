using FruitMarket.Common.Entity;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Products;
using FruitMarket.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;

namespace FruitMarket.Infrastructure.Repositories
{
    public class ProductRepository(FruitMarketDbContext db) : IProductRepository
    {
   public async Task<Product[]> GetUserProduct( int[] productIds)
    {
        var q = (from b in db.Products
                 where  productIds.Contains(b.Id)
                 select b).ToArrayAsync();
                 return await q;
    }

        public async Task<Product> AddAsync(Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return product;
        }

        public ValueTask<Product?> FindAsync(int id)
        {
            return db.Products.FindAsync(id);
        }

        public Task<int> Delete(Product product)
        {
            db.Products.Remove(product);
            return db.SaveChangesAsync();

        }

        public async Task<Product> Update(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return product;
        }

        public async Task<ProductDetailDto[]> Get(
             int? userId,
          string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc)
        {
            var query = db.Products.AsQueryable();

            if (q != null && q != string.Empty)
            {
                query = query.Where(x => x.Name.Contains(q));
            }



            var pQuery = query.Skip(skip).Take(take);
            if (userId != null)
            {

                var nQuery = from p in pQuery
                             join f in db.Favorites.Where(x => x.UserId == userId && x.IsDelete == false) on p.Id equals f.ProductId into qq
                             from qz in qq.DefaultIfEmpty()
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Price = p.Price,
                                 ImageUrl = p.ImageUrl,
                                 IsFavorite = qz != null
                             };
                             if (orderBy == OrderBy.Desc)
                             {
                                nQuery = nQuery.OrderByDescending(x => x.Name);
                             }else{
                                nQuery = nQuery.OrderBy(x => x.Name);
                             }
                return await nQuery.ToArrayAsync();
            }
            else
            {
                var nQuery = from p in pQuery

                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 Name = p.Name,
                                 Price = p.Price,
                                 ImageUrl = p.ImageUrl,
                                 IsFavorite = false
                             };
                               if (orderBy == OrderBy.Desc)
                             {
                                nQuery = nQuery.OrderByDescending(x => x.Name);
                             }else{
                                nQuery = nQuery.OrderBy(x => x.Name);
                             }
                return await nQuery.ToArrayAsync();
            }
        }
    }
}
