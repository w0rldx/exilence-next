namespace Shared.Helpers
{
    using System;
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public static class Password
    {
        public static string Hash(string salt, string password)
        {
            var saltArray = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                saltArray,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return hashed;
        }

        public static string Salt()
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt, 0, salt.Length);
        }

        public static bool Verify(string password, string salt, string hash)
        {
            var passwordHash = Hash(salt, password);
            return passwordHash == hash;
        }
    }
}
