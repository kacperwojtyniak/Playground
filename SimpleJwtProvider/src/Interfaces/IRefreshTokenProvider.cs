using SimpleJwtProvider.Models;
using System;
using System.Threading.Tasks;

namespace SimpleJwtProvider.Interfaces
{
    public interface IRefreshTokenProvider
    {
        Task<RefreshToken> GetRefreshToken(DateTime expDate);
    }
}
