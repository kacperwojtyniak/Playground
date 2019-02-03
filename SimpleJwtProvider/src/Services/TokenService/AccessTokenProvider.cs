using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Services.TokenService
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        private readonly IHeaderProvider _headerProvider;
        private readonly IPayloadProvider _payloadProvider;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public AccessTokenProvider(IHeaderProvider headerProvider, IPayloadProvider payloadProvider)
        {
            this._headerProvider = headerProvider;
            this._payloadProvider = payloadProvider;
        }
        public AccessToken GetAccessToken(DateTime expDate, Dictionary<string, object> claims)
        {
            if (claims == null) throw new ArgumentNullException(typeof(Dictionary<string, object>).ToString());
            
            var token = _jwtSecurityTokenHandler.WriteToken(new JwtSecurityToken(_headerProvider.GetHeader(), _payloadProvider.GetPayload(expDate, claims)));

            return new AccessToken()
            {
                ExpirationDate = expDate,
                TokenValue = token
            };
        }
    }
}
