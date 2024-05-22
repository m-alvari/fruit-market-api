namespace FruitMarket.Domain.Models.Orders;

public record OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public float Price { get; set; }

    public DateTime DataCreation { get; set; }
}
