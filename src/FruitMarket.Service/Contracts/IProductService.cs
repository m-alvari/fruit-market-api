using FruitMarket.Common.Entity;
using FruitMarket.Domain.Models.Products;

namespace FruitMarket.Service.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> AddProductAsync(UpsertProductDto product);
        Task DeleteProductAsync(int id);
        Task<ProductDto?> UpdateProductAsync(UpsertProductDto product, int id);
        Task<ProductDetailDto[]> GetProductAsync(
         string? q = null,
            int take = 8,
            int skip = 0,
            OrderBy orderBy = OrderBy.Asc
   );
        Task<Product?> GetProductWithId(int id);
    }
}
