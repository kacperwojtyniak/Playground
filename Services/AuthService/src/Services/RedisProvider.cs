using AuthService.Configurations;
using AuthService.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class RedisProvider : IRedisProvider
    {
        private readonly RedisConfiguration _config;
        private  ConnectionMultiplexer _redisMultiplexer;

        public RedisProvider(RedisConfiguration config)
        {
            this._config = config;
        }

        public ConnectionMultiplexer GetMultiplexer()
        {
            if (_redisMultiplexer == null)
            {
                _redisMultiplexer = ConnectionMultiplexer.Connect(_config.ConnectionString);
            }
            return _redisMultiplexer;
            
        }
    }
}
