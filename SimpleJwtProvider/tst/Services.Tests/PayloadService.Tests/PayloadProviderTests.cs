using SimpleJwtProvider.Services.PayloadService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimpleJwtProvider.Tests.Services.Tests.PayloadService.Tests
{
    public class PayloadProviderTests
    {
        [Fact]
        public void PayloadProvider_AddsAllClaims()
        {
            var claims = new Dictionary<string, object>();
            claims.Add("iss", "issuerClaim");
            claims.Add("role", "roleClaim");
            claims.Add("userId", 1);
            claims.Add("customClaim", "customClaimValue");

            var expirationTime = new DateTime(2018, 04, 14);

            var provider = new PayloadProvider();

            var payload = provider.GetPayload(expirationTime, claims);
            
            Assert.True(payload.ContainsKey("iss"));
            Assert.True(payload.ContainsKey("role"));
            Assert.True(payload.ContainsKey("userId"));
            Assert.True(payload.ContainsKey("customClaim"));
            Assert.True(payload.ContainsValue("issuerClaim"));
            Assert.True(payload.ContainsValue("roleClaim"));
            Assert.True(payload.ContainsValue(1));
            Assert.True(payload.ContainsValue("customClaimValue"));
        }

        [Fact]
        public void PayloadProvider_SetsCorrectExpirationTime()
        {
            var claims = new Dictionary<string, object>();
            claims.Add("iss", "issuerClaim");

            var expirationTime = new DateTime(2018, 04, 14);

            var provider = new PayloadProvider();

            var payload = provider.GetPayload(expirationTime, claims);

            Assert.True(payload.ContainsKey("exp"));
            Assert.Equal(1523656800, payload.Exp);
        }
    }
}
