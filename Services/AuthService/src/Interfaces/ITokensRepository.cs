using AuthService.Requests;
using SimpleJwtProvider.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface ITokensRepository
    {
        Task<RedisValue> GetUserRole(string tokenValue);
        Task AddTokenAsync(RefreshToken token, string role);
    }
}
