using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisPlayground.Interfaces;
using RedisPlayground.Models;
using StackExchange.Redis;

namespace RedisPlayground.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StackController : ControllerBase
    {
        private readonly IRedisDatabaseProvider _redisDatabaseProvider;

        public StackController(IRedisDatabaseProvider redisDatabaseProvider)
        {
            _redisDatabaseProvider = redisDatabaseProvider;
        }

        [HttpPut]
        public IActionResult SetString([FromBody]CacheEntry entry)
        {
            var db = _redisDatabaseProvider.GetDatabase();
            db.StringSet(entry.Key, entry.Value);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetString(string key)
        {
            var db = _redisDatabaseProvider.GetDatabase();
            var value = db.StringGet(key);
            return Ok(value);
        }
    }
}
