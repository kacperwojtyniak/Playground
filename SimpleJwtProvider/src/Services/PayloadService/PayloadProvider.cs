using SimpleJwtProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Services.PayloadService
{
    public class PayloadProvider : IPayloadProvider
    {
        public JwtPayload GetPayload(DateTime expirationTime, Dictionary<string, object> claims)
        {
            var payload = new JwtPayload();
            AddExpirationClaim(payload, expirationTime);

            foreach (var claim in claims)
            {
                payload.Add(claim.Key, claim.Value);
            }

            return payload;
        }

        private void AddExpirationClaim(JwtPayload payload, DateTime expirationTime)
        {
            var exp = GetExpirationTime(expirationTime);
            payload.Add("exp", exp);
        }

        private long GetExpirationTime(DateTime expDate)
        {
            var expires = expDate.ToUniversalTime();
            var centuryBegin = new DateTime(1970, 1, 1);
            return (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
        }
    }
}
