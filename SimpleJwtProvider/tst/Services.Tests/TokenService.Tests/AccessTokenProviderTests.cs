using Moq;
using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Services.TokenService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xunit;

namespace SimpleJwtProvider.Tests.Services.Tests.TokenService.Tests
{
    public class AccessTokenProviderTests
    {

        [Fact]
        public void AccesTokenProvider_ThrowsArgumentNullException_ClaimsDictionaryIsNull()
        {
            
            var headerProviderMock = new Mock<IHeaderProvider>();
            
            var payloadProviderMock = new Mock<IPayloadProvider>();
            
            var tokenProvider = new AccessTokenProvider(headerProviderMock.Object, payloadProviderMock.Object);

            Assert.Throws<ArgumentNullException>(()=> tokenProvider.GetAccessToken(It.IsAny<DateTime>(), It.IsAny<Dictionary<string, object>>()));

        }

        [Fact]
        public void AccessTokenProvider_GetAccessToken_ReturnsTokenWithExpirationDataSet()
        {
            var expirationData = new DateTime(2018, 04, 14);
            var header = new JwtHeader();            
            var headerProviderMock = new Mock<IHeaderProvider>();
            headerProviderMock.Setup(x => x.GetHeader()).Returns(header);

            var payload = new JwtPayload();
            var payloadProviderMock = new Mock<IPayloadProvider>();
            payloadProviderMock.Setup(x => x.GetPayload(It.IsAny<DateTime>(), It.IsAny<Dictionary<string, object>>())).Returns(payload);

            var tokenProvider = new AccessTokenProvider(headerProviderMock.Object, payloadProviderMock.Object);

            var token = tokenProvider.GetAccessToken(expirationData, new Dictionary<string, object>());

            Assert.Equal(expirationData, token.ExpirationDate);
        

        }

    }
}
