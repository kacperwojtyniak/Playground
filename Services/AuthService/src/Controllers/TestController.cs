using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Interfaces;
using Database.Common.ContextProvider;
using Database.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace AuthService.Controllers
{
   
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IContextProvider _contextProvider;
        private readonly IRedisProvider _redisProvider;

        public TestController(IContextProvider contextProvider, IRedisProvider redisProvider)
        {
            this._contextProvider = contextProvider;
            this._redisProvider = redisProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<TstModel> result;
            using (var context = _contextProvider.GetContext<TstModel>())
            {
                 result = await context.GetAllAsync();                
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertRedis([FromBody]TstModel model)
        {
            var db = _redisProvider.GetMultiplexer().GetDatabase();
            
            var entries = new HashEntry[] {
            new HashEntry("Role", model.Role),
            new HashEntry("Password", model.Password)};

            try
            {
                db.HashSet(model.Login, entries);
            }
            catch (Exception)
            {

                throw;
            }
            

            return Ok();
        }

        [HttpGet]
        public IActionResult GetHash(string userName)
        {
            var db = _redisProvider.GetMultiplexer().GetDatabase();

            

            try
            {
                var result = db.HashGetAll(userName);                
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}