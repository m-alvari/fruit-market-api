using FruitMarket.Domain.Models.Auth;

namespace FruitMarket.Domain.Contracts;

public interface IJwtManagerRepository
{
    LoginTokenResponseDto? GenerateToken(
        int userId,
        string username,
        string email,
        string firstName,
        string lastName
    );

    (JwtTokenDto? Jet, bool HasError) Load(string tokenString);


    JwtTokenDto? GetToken();
}


