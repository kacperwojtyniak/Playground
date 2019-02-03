using PasswordHasher.Models;

namespace PasswordHasher.Interfaces
{
    public interface IPasswordHasher
    {

        HashedPassword HashPassword(string password);
        HashedPassword HashPassword(string password, byte[] salt);
    }
}
