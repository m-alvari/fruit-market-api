using FruitMarket.Common.Entity;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Products;
using FruitMarket.Service.Contracts;

namespace FruitMarket.Service.Services;

public class ProductService(IProductRepository repo, ICurrentUserService currentUserService) : IProductService
{


    public async Task<Product?> GetProductWithId(int id)
    {
        var product = await repo.FindAsync(id);
        if (product != null)
        {
            return product;
        }
        return null;

    }

    public async Task<ProductDetailDto[]> GetProductAsync(
          string? q = null,
             int take = 8,
             int skip = 0,
             OrderBy orderBy = OrderBy.Asc
    )
    {
        int? userId = currentUserService.IsLogin() ? currentUserService.User.UserId : null;
        var list = await repo.Get(userId, q, take, skip, orderBy);
        return list;
    }
    public async Task<ProductDto> AddProductAsync(UpsertProductDto product)
    {
        var entity = new Product
        {
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price
        };

        var newProduct = await repo.AddAsync(entity);

        return new ProductDto
        {
            Id = newProduct.Id,
            Name = newProduct.Name,
            Price = newProduct.Price,
            ImageUrl = newProduct.ImageUrl
        };
    }

    public async Task DeleteProductAsync(int id)
    {
        var entity = await repo.FindAsync(id);
        if (entity != null)
        {
            await repo.Delete(entity);
        }
    }

    public async Task<ProductDto?> UpdateProductAsync(UpsertProductDto product, int id)
    {
        var entity = await repo.FindAsync(id);
        if (entity != null)
        {
            entity.Name = product.Name;
            entity.ImageUrl = product.ImageUrl;
            entity.Price = product.Price;

            await repo.Update(entity);
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
            };
        }
        else
        {
            return null;
        }

    }
}
