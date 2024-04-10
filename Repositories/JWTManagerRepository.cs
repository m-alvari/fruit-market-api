using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using fruit_market_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace fruit_market_api.Repositories;

public class JWTManagerRepository : IJWTManagerRepository
{
    private readonly IConfiguration _iconfiguration;

    public JWTManagerRepository(IConfiguration iconfiguration)
    {
        _iconfiguration = iconfiguration;
    }


   public LoginTokenResponse GenerateToken(int userId, string username, string email, string firstName, string lastName)
    {
          try
        {
        // Your secret key
         var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
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

          var refreshToken = GenerateRefreshToken();
            return new LoginTokenResponse { AccessToken = tokenString, RefreshToken = refreshToken };
              }
        catch (Exception ex)
        {
            return null;
        }
    }


    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
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
                Convert.ToInt32(jwtToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Name).First().Value),
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

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var Key = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}