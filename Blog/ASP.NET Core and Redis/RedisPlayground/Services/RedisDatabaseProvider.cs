using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisPlayground.Interfaces;
using StackExchange.Redis;

namespace RedisPlayground.Services
{
    public class RedisDatabaseProvider : IRedisDatabaseProvider
    {
        
        private ConnectionMultiplexer _redisMultiplexer;

        public IDatabase GetDatabase()
        {
            if (_redisMultiplexer == null)
            {
                _redisMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6736");
            }

            return _redisMultiplexer.GetDatabase();

        }
    }
}
