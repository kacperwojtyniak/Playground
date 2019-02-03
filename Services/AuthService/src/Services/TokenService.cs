using AuthService.Configurations;
using AuthService.Interfaces;
using AuthService.Requests;
using AuthService.Responses;
using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class TokenService : ITokenService
    {

        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly ITokensRepository _tokensRepository;
        private readonly TokensConfiguration _config;
        private readonly Dictionary<string, object> _claims;

        public TokenService(IAccessTokenProvider accessTokenProvider,
            IRefreshTokenProvider refreshTokenProvider,
            ITokensRepository tokensRepository,
            TokensConfiguration config)
        {
            this._accessTokenProvider = accessTokenProvider;
            this._refreshTokenProvider = refreshTokenProvider;
            this._tokensRepository = tokensRepository;
            this._config = config;
            this._claims = ConfigureClaims();
        }
        public async Task<AccessToken> GetAccessTokenAsync(AccessTokenRequest request)
        {
            //TODO Refactor
            var userRole = await _tokensRepository.GetUserRole(request.RefreshToken);

            if (userRole.IsNullOrEmpty) throw new ArgumentException(nameof(request.RefreshToken));


            _claims.Add("role", userRole.ToString());
            var result = _accessTokenProvider.GetAccessToken(DateTime.Now.AddHours(_config.AccessTokenExpMinutes), _claims);
            return result;


        }

        public async Task<LogInResponse> LogInAsync(string userRole)
        {
            var refreshToken = _refreshTokenProvider.GetRefreshToken(DateTime.Now.AddMinutes(_config.RefreshTokenExpHours));

            _claims.Add("role", userRole);

            var accessToken = _accessTokenProvider.GetAccessToken(DateTime.Now.AddMinutes(_config.AccessTokenExpMinutes), _claims);

            //TODO Apply CQRS
            _tokensRepository.AddTokenAsync(await refreshToken, userRole);

            return new LogInResponse()
            {
                AccessToken = accessToken,
                RefreshToken = await refreshToken
            };
        }

        private Dictionary<string, object> ConfigureClaims()
        {
            var claims = new Dictionary<string, object>();

            claims.Add("iss", "DemoAuthorizationService");

            return claims;

        }
    }
}
