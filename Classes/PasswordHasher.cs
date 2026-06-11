using System;
using System.Linq;
using System.Security.Cryptography;

namespace SpectrumWebForms.Data
{
    internal static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static string CreateSalt()
        {
            var salt = new byte[SaltSize];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return Convert.ToBase64String(deriveBytes.GetBytes(HashSize));
            }
        }

        public static bool VerifyPassword(string password, string saltBase64, string expectedHash)
        {
            var actualHash = HashPassword(password, saltBase64);
            return FixedTimeEquals(actualHash, expectedHash);
        }

        private static bool FixedTimeEquals(string left, string right)
        {
            if (left == null || right == null)
            {
                return false;
            }

            var leftBytes = Convert.FromBase64String(left);
            var rightBytes = Convert.FromBase64String(right);

            if (leftBytes.Length != rightBytes.Length)
            {
                return false;
            }

            var diff = 0;
            for (var index = 0; index < leftBytes.Length; index++)
            {
                diff |= leftBytes[index] ^ rightBytes[index];
            }

            return diff == 0;
        }
    }
}