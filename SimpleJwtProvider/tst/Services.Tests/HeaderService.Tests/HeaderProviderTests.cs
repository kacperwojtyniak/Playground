using SimpleJwtProvider.Configuration;
using SimpleJwtProvider.Services.HeaderService;
using SimpleJwtProvider.Services.HeaderService.HeaderCreators;
using SimpleJwtProvider.Tests.TestModels;
using System;
using System.IO;
using Xunit;

namespace SimpleJwtProvider.Tests.Services.Tests.HeaderProvider.Tests
{
    public class HeaderProviderTests
    {
        [Fact]
        public void HeaderProvider_ThrowsArgumentNullException_ConfigIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HeaderProvider<TestHeaderCreator>(null));
        }

        [Fact]
        public void HeaderProvider_ThrowsArgumentException_SigningKeyNull()
        {
            var config = new SimpleJwtProviderConfig();
            config.SigningKey = null;            

            Assert.Throws<ArgumentException>(() => new HeaderProvider<TestHeaderCreator>(config));
        }        

        [Fact]
        public void HeaderProviderWithHmacFactory_ReturnsHmacSignedKey()
        {
           
            var config = new SimpleJwtProviderConfig();
            config.SigningKey = "ThisIsMyIncrediblyDifficultHMACSigningKeyUsedInTests!";           
            var provider = new HeaderProvider<HMACHeaderCreator>(config);

            var header = provider.GetHeader();
            
            Assert.Equal("HS256", header.Alg);

        }

        [Fact]
        public void HeaderProviderWithRsaFactory_ReturnsRsaSignedKey()
        {          
            var keyPath =Path.GetFullPath("..\\..\\..\\TstCertificate\\private-rsa.xml");
            var config = new SimpleJwtProviderConfig();
            config.SigningKey = keyPath;            
            var provider = new HeaderProvider<RSAHeaderCreator>(config);
            
            var header = provider.GetHeader();
            
            Assert.Equal("RS256", header.Alg);
        }
    }
}
