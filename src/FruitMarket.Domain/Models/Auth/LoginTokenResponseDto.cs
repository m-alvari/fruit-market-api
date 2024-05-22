namespace FruitMarket.Domain.Models.Auth;

public record LoginTokenResponseDto
{
    public required string AccessToken { get; set; }
}
