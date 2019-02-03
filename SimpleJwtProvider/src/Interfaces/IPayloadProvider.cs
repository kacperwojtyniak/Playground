using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Interfaces
{
    public interface IPayloadProvider
    {
        JwtPayload GetPayload(DateTime expirationTime, Dictionary<string, object> claims);
    }
}
