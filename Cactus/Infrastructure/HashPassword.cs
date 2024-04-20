using System.Security.Cryptography;
using System.Text;

namespace Cactus.Infrastructure
{
    public static class HashPassword
    {
        public static string Generate(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashed).Replace("-", "").ToLower();
            }
        }
    }
}
