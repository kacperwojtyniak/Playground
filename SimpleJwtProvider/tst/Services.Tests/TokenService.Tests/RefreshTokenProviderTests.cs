using SimpleJwtProvider.Services.TokenService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimpleJwtProvider.Tests.Services.Tests.TokenService.Tests
{
    public class RefreshTokenProviderTests
    {

        [Fact]
        public async void RefreshTokenProvider_GetRefreshToken_ReturnsTokenWithExpirationDataSet()
        {
            var expirationData = new DateTime(2018, 04, 14);
            var refreshTokenProvider = new RefreshTokenProvider();

            var token = await refreshTokenProvider.GetRefreshToken(expirationData);

            Assert.Equal(expirationData, token.ExpirationDate);
        }

        [Fact]
        public async void RefreshTokenProvider_GetRefreshToken_ReturnsTokenWithTokenValueSet()
        {
            var expirationData = new DateTime(2018, 04, 14);
            var refreshTokenProvider = new RefreshTokenProvider();

            var token = await refreshTokenProvider.GetRefreshToken(expirationData);

            Assert.NotNull(token);
            Assert.NotEmpty(token.TokenValue);
        }

        [Fact]
        public async void RefreshTokenProvider_GetRefreshToken_ReturnsTokenIsNotRevoked()
        {

            var expirationData = new DateTime(2018, 04, 14);
            var refreshTokenProvider = new RefreshTokenProvider();

            var token = await refreshTokenProvider.GetRefreshToken(expirationData);

            Assert.False(token.Revoked);

        }
    }
}
