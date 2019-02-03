using SimpleJwtProvider.Configuration;
using SimpleJwtProvider.Factories;
using SimpleJwtProvider.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleJwtProvider.Services.HeaderService
{
    public class HeaderProvider<T> : IHeaderProvider 
        where T : HeaderCreator, new()
    {
        private readonly SimpleJwtProviderConfig _config;
        
        private readonly T _provider;
        private JwtHeader _header;

        public HeaderProvider(SimpleJwtProviderConfig config)
        {
            
            if (config == null) throw new ArgumentNullException(typeof(SimpleJwtProviderConfig).ToString());
            if (string.IsNullOrEmpty(config.SigningKey)) throw new ArgumentException("Signing key is null or empty!");
           
            this._config = config;
            this._provider = new T();
        }
        
        private void CreateHeader()
        {
            _header = this._provider.CreateHeader(_config.SigningKey);
        }
       
        public JwtHeader GetHeader()
        {
            if (_header == null)
            {
                CreateHeader();
            }
            return _header;
        }
    }
}
