using System.Security.Cryptography;
using System.Text;

namespace Products.BusinessLayer.Util
{
    public static class PasswordUtil
    {
        public static string HashPassword(string password, string salt)
        {
            using (var hmac = new HMACSHA512())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                return Encoding.Default.GetString(hmac.ComputeHash(passwordBytes));
            }

        }
    }
}
