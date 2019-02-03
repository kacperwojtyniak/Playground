using Microsoft.IdentityModel.Tokens;
using SimpleJwtProvider.Extensions;
using SimpleJwtProvider.Factories;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;

namespace SimpleJwtProvider.Services.HeaderService.HeaderCreators
{
    public class RSAHeaderCreator : HeaderCreator
    {        
        public override JwtHeader CreateHeader(string signingKeyPath)
        {
            using (RSA rsaKey = RSA.Create())
            {
                var xmlKey = File.ReadAllText(signingKeyPath);
                rsaKey.CustFromXmlString(xmlKey);
                var signingKey = new RsaSecurityKey(rsaKey);
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);
                return new JwtHeader(signingCredentials);
            }
        }
    }
}
