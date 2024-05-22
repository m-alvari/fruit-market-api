namespace FruitMarket.Domain.Models.Auth;

public record JwtTokenDto
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set;}
    public required string Email { get; set; }
}
