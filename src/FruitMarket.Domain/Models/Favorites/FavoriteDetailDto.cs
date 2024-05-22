namespace FruitMarket.Domain.Models.Favorites;

public record FavoriteDetailDto
{
    public int ProductId { get; set; }
    public required string imageUrl { get; set; }
    public required string Name { get; set; }
    public float Price { get; set; }
}
