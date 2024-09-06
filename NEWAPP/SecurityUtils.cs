
using System.Drawing;
using System.Security.Cryptography;
namespace NEWAPP
{
     public static class SecurityUtils
    {
        public static string Generate256BitKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(key);
                }
                return Convert.ToBase64String(key);
            }
        }

    }
}
