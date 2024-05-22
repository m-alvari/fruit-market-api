namespace FruitMarket.Domain.Models.Baskets;

public record BasketDetailDto
{

    public int ProductId { get; set; }
    public int Count { get; set; }
    public DateTime DateCreation { get; set; }
    public required string Name { get; set; }
    public float Price { get; set; }
    public required string ImageUrl  { get; set; }
}
