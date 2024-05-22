
namespace FruitMarket.Domain.Models.Favorites;

public record Favorite
{

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public bool IsDelete { get; set; }

}
