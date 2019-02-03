using AuthService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AuthService.Tests.ServicesTests
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("abc")]
        [InlineData("veryStrongPassword")]
        public void HashPassword_GeneratesRandomSaltEachTime(string password)
        {
            var passwordHasher = new PasswordHasher();

            var hash1 = passwordHasher.HashPassword(password);
            var hash2 = passwordHasher.HashPassword(password);

            Assert.NotEqual(hash1.PasswordHash, hash2.PasswordHash);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("veryStrongPassword")]
        public void HashPassword_UsesProvidedSalt_HashesAreEqual(string password)
        {
            byte[] salt = new byte[128 / 8];
            var rnd = new Random();
            rnd.NextBytes(salt);

            var passwordHasher = new PasswordHasher();

            var hash1 = passwordHasher.HashPassword(password, salt);
            var hash2 = passwordHasher.HashPassword(password, salt);

            Assert.Equal(hash1.PasswordHash, hash2.PasswordHash);
        }
    }
}
