using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RedisPlayground.Models;

namespace RedisPlayground.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DistributedController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public DistributedController(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }

        [HttpPut]
        public IActionResult SetValue([FromBody]CacheEntry entry)
        {
            this._distributedCache.SetString(entry.Key, entry.Value);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetValue(string key)
        {
            string value = this._distributedCache.GetString(key);
            return Ok(value);
        }
    }
}
