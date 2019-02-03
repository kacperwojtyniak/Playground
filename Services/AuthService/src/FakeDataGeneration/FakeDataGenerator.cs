using AuthService.Interfaces;
using Database.Common.ContextProvider;
using Database.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthService.FakeDataGeneration
{
    public class FakeDataGenerator : IFakeDataGenerator
    {
        private readonly IContextProvider _provider;
        private readonly IPasswordHasher _hasher;

        public FakeDataGenerator(IContextProvider provider, IPasswordHasher hasher)
        {
            this._provider = provider;
            this._hasher = hasher;
        }
        public void GenerateFakeUsers()
        {
            using (var context = _provider.GetContext<UserModel>())
            {
                context.AddManyAsync(GetUsers());
            }
        }

        private IEnumerable<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();

            var passwordHash1 = _hasher.HashPassword("abc");
            users.Add(new UserModel() { UserLogin = "User1", Password = passwordHash1.PasswordHash, Salt = passwordHash1.SaltBase64, Role = "Admin" });

            var passwordHash2 = _hasher.HashPassword("zaqwertyu");
            users.Add(new UserModel() { UserLogin = "User1", Password = passwordHash2.PasswordHash, Salt = passwordHash2.SaltBase64, Role = "User" });

            return users;

        }

        private string GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
