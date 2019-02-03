using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
   public interface IPasswordValidator
    {
        bool ValidatePassword(string password, string passwordHash, string salt);
    }
}
