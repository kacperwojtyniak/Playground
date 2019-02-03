using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface IPasswordHasher
    {

        HashedPassword HashPassword(string password);
        HashedPassword HashPassword(string password, byte[] salt);
    }
}
