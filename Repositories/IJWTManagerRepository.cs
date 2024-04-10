using System.Security.Claims;
using fruit_market_api.Models;

namespace fruit_market_api.Repositories
{
    public interface IJWTManagerRepository
    {
        LoginTokenResponse GenerateToken(int userId, string username, string email, string firstName, string lastName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        (JwtToken? Jet, bool HasError) Load(string tokenString);
    }
}