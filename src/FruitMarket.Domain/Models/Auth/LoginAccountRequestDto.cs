namespace FruitMarket.Domain.Models.Auth;

public record LoginAccountRequestDto
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
