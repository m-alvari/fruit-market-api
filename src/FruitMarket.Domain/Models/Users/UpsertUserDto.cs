namespace FruitMarket.Domain.Models.Users;

public record UpsertUserDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public required string Email { get; set; }
    public GenderType Gender { get; set; }
    public required string PhoneNumber { get; set; }
    public  string? ImageProfile { get; set; }
    public  string? Address { get; set; }
    public required string Password { get; set; }
}
