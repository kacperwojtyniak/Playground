using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Factories
{
    public abstract class HeaderCreator
    {
        public abstract JwtHeader CreateHeader(string signingKey);
    }
}
