using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Interfaces
{
    public interface IHeaderProvider
    {
        JwtHeader GetHeader();
    }
}
