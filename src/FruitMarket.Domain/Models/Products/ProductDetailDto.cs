using FruitMarket.Common.Entity;

namespace FruitMarket.Domain.Models.Products;

public record ProductDetailDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public float Price { get; set; }
    public required string ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
}
