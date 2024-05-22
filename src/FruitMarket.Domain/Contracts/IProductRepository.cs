using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Products;

namespace FruitMarket.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        ValueTask<Product?> FindAsync(int id);
        Task<int> Delete(Product product);
        Task<Product> Update(Product product);
        Task<ProductDetailDto[]> Get(
            int? userId,
            string? q = null,
            int take = 8,
            int skip = 0,
            OrderBy orderBy = OrderBy.Asc);

        Task<Product[]> GetUserProduct(int[] productIds);
    }
}

