namespace FruitMarket.Domain.Models.Orders;

public record OrderWithDetail
{
    public float Price { get; set; }
    public DateTime DataCreation { get; set; }
    public required OrderDetailDto[] Details { get; set; }
}
