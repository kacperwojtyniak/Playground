using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PasswordHasher.Interfaces;
using PasswordHasher.Models;

namespace PasswordHasher.Services
{
    public class PasswordHasher : IPasswordHasher
    {

        public HashedPassword HashPassword(string password)
        {
            return HashPassword(password, GetSalt());
        }

        public HashedPassword HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return new HashedPassword()
            {
                PasswordHash = hashed,
                SaltBase64 = SaltToBase64(salt)
            };
        }

        private string SaltToBase64(byte[] salt)
        {
            return Convert.ToBase64String(salt);
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

    }
}
