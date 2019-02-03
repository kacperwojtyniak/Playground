using SimpleJwtProvider.Factories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SimpleJwtProvider.Tests.TestModels
{
    public class TestHeaderCreator : HeaderCreator
    {
        public override JwtHeader CreateHeader(string signingKey)
        {
            throw new NotImplementedException();
        }
    }
}
