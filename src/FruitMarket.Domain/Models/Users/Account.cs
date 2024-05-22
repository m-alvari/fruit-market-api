namespace FruitMarket.Domain.Models.Users;

public record Account
{
    public int UserId { get; set; }

    public required string Password { get; set; }
}
