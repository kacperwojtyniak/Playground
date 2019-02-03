using AuthService.Interfaces;
using SimpleJwtProvider.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class TokensRepository : ITokensRepository
    {
        private readonly IRedisProvider _redisProvider;

        public TokensRepository(IRedisProvider redisProvider)
        {
            this._redisProvider = redisProvider;
        }
        public async Task AddTokenAsync(RefreshToken token, string role)
        {
            var db = _redisProvider.GetMultiplexer().GetDatabase();
            var ticksToExpire = (token.ExpirationDate - DateTime.Now).Ticks;
            await db.StringSetAsync(token.TokenValue, role, new TimeSpan(ticksToExpire));

        }

        public async Task<RedisValue> GetUserRole(string tokenValue)
        {
            var db = _redisProvider.GetMultiplexer().GetDatabase();

            return await db.StringGetAsync(tokenValue);            

        }
    }
}
