using AuthService.Configurations;
using AuthService.Interfaces;
using AuthService.Services;
using Moq;
using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Tests.ServicesTests
{
    public class TokenServiceTests
    {
                
        [Fact]
        public void LogIn_AccessToken_IsNotNull()            
        {
            var accessTokenProviderMock = new Mock<IAccessTokenProvider>();
            accessTokenProviderMock.Setup(x => x.GetAccessToken(It.IsAny<DateTime>(), It.IsAny<Dictionary<string, object>>())).Returns(new AccessToken());

            var refreshTokenProviderMock = new Mock<IRefreshTokenProvider>();
            var tokensRepositoryMock = new Mock<ITokensRepository>();
            var configMock = new Mock<TokensConfiguration>();

            var tokenService = new TokenService(accessTokenProviderMock.Object, refreshTokenProviderMock.Object, tokensRepositoryMock.Object, configMock.Object);

            var result = tokenService.LogInAsync(It.IsAny<string>());

            Assert.NotNull(result.Result.AccessToken);
        }

        [Fact]
        public void LogIn_RefreshToken_IsNotNull()
        {
            var accessTokenProviderMock = new Mock<IAccessTokenProvider>();

            var refreshTokenProviderMock = new Mock<IRefreshTokenProvider>();
            refreshTokenProviderMock.Setup(x => x.GetRefreshToken(It.IsAny<DateTime>())).Returns(Task.FromResult(new RefreshToken()));
            var tokensRepositoryMock = new Mock<ITokensRepository>();
            var configMock = new Mock<TokensConfiguration>();

            var tokenService = new TokenService(accessTokenProviderMock.Object, refreshTokenProviderMock.Object, tokensRepositoryMock.Object, configMock.Object);

            var result = tokenService.LogInAsync(It.IsAny<string>());

            Assert.NotNull(result.Result.RefreshToken);
        }

    }
}
