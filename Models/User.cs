namespace fruit_market_api.Models;



public record User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Email { get; set; }
    public GenderType Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ImageProfile { get; set; }
}


public enum GenderType
{
    Unknown,
    Male,
    Female
}

