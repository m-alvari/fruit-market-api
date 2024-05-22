
namespace FruitMarket.Domain.Models.Products;

public record UpsertProductDto
{
    public required string Name { get; set; }
    public float Price { get; set; }
    public required string ImageUrl { get; set; }
}
