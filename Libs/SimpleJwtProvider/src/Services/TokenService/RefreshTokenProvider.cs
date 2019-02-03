using SimpleJwtProvider.Interfaces;
using SimpleJwtProvider.Models;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleJwtProvider.Services.TokenService
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        public async Task<RefreshToken> GetRefreshToken(DateTime expDate)
        {
           return await Task.Run(() =>
            {
                byte[] rnd = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(rnd);
                }

                string tokenValue = Convert.ToBase64String(rnd);

                return new RefreshToken()
                {
                    TokenValue = tokenValue,
                    Revoked = false,
                    ExpirationDate = expDate
                };
            });
            
        }
    }
}
