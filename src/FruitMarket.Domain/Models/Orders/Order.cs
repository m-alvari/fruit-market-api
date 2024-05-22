using FruitMarket.Common.Entity;

namespace FruitMarket.Domain.Models.Orders;

public record Order : EntityBase
{
    public int UserId { get; set; }
    public float Price { get; set; }

    public DateTime DataCreation { get; set; }
}
