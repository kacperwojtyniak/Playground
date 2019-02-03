namespace PasswordHasher.Models
{
    public class HashedPassword
    {
        public string PasswordHash { get; set; }
        public string SaltBase64 { get; set; }
    }
}
