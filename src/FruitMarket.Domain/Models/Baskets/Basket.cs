namespace FruitMarket.Domain.Models.Baskets;

public record Basket
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public DateTime DateCreation { get; set; }

}
