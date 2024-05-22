namespace FruitMarket.Domain.Models.Favorites;

public record FavoriteDto
{

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public bool IsDelete { get; set; }

}
