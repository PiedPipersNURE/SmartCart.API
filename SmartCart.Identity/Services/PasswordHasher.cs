using System.Security.Cryptography;

namespace SmartCart.Identity.Services
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32;
        private const int Iterations = 10000;

        public static (string hash, string salt) HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[SaltSize];
                rng.GetBytes(saltBytes);

                var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
                byte[] hashBytes = pbkdf2.GetBytes(KeySize);

                string salt = Convert.ToBase64String(saltBytes);
                string hash = Convert.ToBase64String(hashBytes);

                return (hash, salt);
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            byte[] hashBytes = pbkdf2.GetBytes(KeySize);

            string hash = Convert.ToBase64String(hashBytes);
            return hash == storedHash;
        }
    }
}
