using AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly IPasswordHasher _passwordHasher;

        public PasswordValidator(IPasswordHasher passwordHasher)
        {
            this._passwordHasher = passwordHasher;
        }
        public bool ValidatePassword(string password, string passwordHash, string salt)
        {
            try
            {
                byte[] saltBytes = Base64StringToByteArray(salt);

                string hashed = _passwordHasher.HashPassword(password, saltBytes).PasswordHash;

                return hashed == passwordHash;
            }
            catch (Exception ex)
            {
                //#TODO log
                throw;
            }
        }

        private byte[] Base64StringToByteArray(string salt)
        {
            return Convert.FromBase64String(salt);
        }
    }
}
