using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisPlayground.Interfaces;
using RedisPlayground.Models;

namespace RedisPlayground.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LockController : ControllerBase
    {
        private readonly IRedisDatabaseProvider _redisDatabaseProvider;

        public LockController(IRedisDatabaseProvider redisDatabaseProvider)
        {
            _redisDatabaseProvider = redisDatabaseProvider;
        }

        [HttpPut]
        public IActionResult IncrementEntry([FromBody] CacheEntry entry)
        {
            var db = _redisDatabaseProvider.GetDatabase();
            var value = int.Parse(db.StringGet(entry.Key));
            value = value + 1;
            db.StringSet(entry.Key, value);
            return Ok();
        }
    }
}
