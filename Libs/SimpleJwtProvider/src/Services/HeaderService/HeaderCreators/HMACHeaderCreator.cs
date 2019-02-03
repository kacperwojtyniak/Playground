using Microsoft.IdentityModel.Tokens;
using SimpleJwtProvider.Factories;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SimpleJwtProvider.Services.HeaderService.HeaderCreators
{
    public class HMACHeaderCreator : HeaderCreator
    {        
        public override JwtHeader CreateHeader(string signingKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            return new JwtHeader(signingCredentials);
        }
    }
}
