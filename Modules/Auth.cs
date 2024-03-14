using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace fruit_market_api.Modules;


public class Auth
{
    const string secretKey = "9ZAZi2xjlvpvnORt8u2w1VNdUC0quu+d099De7LK7nM=";
    public string GenerateToken(int userId, string username, string email, string firstName, string lastName)
    {
        // Your secret key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // Create a signing credential using the secret key
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // Create claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("firstName", firstName),
            new Claim("lastName", lastName),
            new Claim(JwtRegisteredClaimNames.Jti, userId.ToString())
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

        return tokenString;
    }


    public (JwtToken? Jet, bool HasError) Load(string tokenString)
    {
        JwtToken? jwt = null;
        bool hasError = false;

        // Create an instance of JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Read the token string and parse it into JwtSecurityToken object
            var jwtToken = tokenHandler.ReadJwtToken(tokenString);

            // Extract claims from the token

            jwt = new JwtToken(
                Convert.ToInt32(jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Jti).First().Value),
                 jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub).First().Value,
                jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Email).First().Value,
                jwtToken.Claims.Where(x => x.Type == "firstName").First().Value,
                jwtToken.Claims.Where(x => x.Type == "lastName").First().Value
            );

        }
        catch (Exception ex)
        {
            hasError = true;
            Console.WriteLine($"Failed to extract data from token: {ex.Message}");
        }
        return (jwt, hasError);
    }
}



public record JwtToken(int UserId, string Username, string Email, string FirstName, string LastName);