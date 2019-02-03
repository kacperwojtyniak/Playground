namespace SimpleJwtProvider.Models
{
    public class RefreshToken : Token
    {        
        public bool Revoked { get; set; }
    }
}
