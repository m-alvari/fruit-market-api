using System.Security.Cryptography;
using System.Text;

namespace FruitMarket.Common.Extensions;

    public static class AuthExtensions
    {
        public static string GetHash(this string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
