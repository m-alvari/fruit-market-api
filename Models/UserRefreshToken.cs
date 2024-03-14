namespace fruit_market_api.Models;


public class UserRefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string RefreshToken { get; set; }

    public bool IsActive { get; set; }
}
