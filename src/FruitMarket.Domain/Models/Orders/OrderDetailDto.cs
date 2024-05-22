namespace FruitMarket.Domain.Models.Orders;

public record OrderDetailDto
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }

}
