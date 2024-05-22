using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FruitMarket.Domain.Contracts;
using FruitMarket.Domain.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FruitMarket.Infrastructure.Repositories;

public class JwtManagerRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IJwtManagerRepository
{
    public  LoginTokenResponseDto? GenerateToken(
        int userId,
        string username,
        string email,
        string firstName,
        string lastName
    )
    {
        try
        {
            // Your secret key
            var tokenKey = Encoding.UTF8.GetBytes(configuration["jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(tokenKey);

            // Create a signing credential using the secret key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Create claims
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim(JwtRegisteredClaimNames.Name, userId.ToString())
        };

            // Create a JWT token
            var token = new JwtSecurityToken(
                issuer: "fruit-market-api",
                audience: "fruit-market-api",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // Token expires in 1 day
                signingCredentials: credentials);

            // Serialize the token to a string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginTokenResponseDto { AccessToken = tokenString };
        }
        catch (Exception)
        {
            return null;
        }
    }


    public (JwtTokenDto? Jet, bool HasError) Load(string tokenString)
    {
        JwtTokenDto? jwt = null;
        bool hasError = false;

        // Create an instance of JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Read the token string and parse it into JwtSecurityToken object
            var jwtToken = tokenHandler.ReadJwtToken(tokenString);

            // Extract claims from the token

            jwt = new JwtTokenDto()
            {
                UserId = Convert.ToInt32(jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Name).First().Value),
                UserName = jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).First().Value,
                Email = jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Email).First().Value,
                FirstName = jwtToken.Claims.Where(x => x.Type == "firstName").First().Value,
                LastName = jwtToken.Claims.Where(x => x.Type == "lastName").First().Value
            };

        }
        catch (Exception ex)
        {
            hasError = true;
            Console.WriteLine($"Failed to extract data from token: {ex.Message}");
        }
        return (jwt, hasError);
    }


     public JwtTokenDto? GetToken()
    {

        JwtTokenDto? jwtToken = null;
        // Read access token from HttpContext
        var accessToken = httpContextAccessor?.HttpContext.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(accessToken))
        {
            var tokenData = accessToken?.ToString().Split(' ')[1];
            var result = Load(tokenData);
            if (!result.HasError)
            {
                jwtToken = result.Jet;
            }
        }

        return jwtToken;

    }

}
