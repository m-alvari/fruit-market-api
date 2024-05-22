
using FruitMarket.Common.Entity;

namespace FruitMarket.Domain.Models.Products;

public record Product : EntityBase
{
    public required string Name { get; set; }
    public float Price { get; set; }
    public required string ImageUrl { get; set; }
}
