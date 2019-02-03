using AuthService.Interfaces;
using AuthService.Requests;
using Database.Common.ContextProvider;
using Database.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authorizationService;
        private readonly IContextProvider contextProvider;

        public AuthController(IAuthenticationService authorizationService)
        {
            this._authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]LogInRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException();                
                var result = _authorizationService.LogInAsync(request);
                return Ok(await result);
            }
            catch (ArgumentException ex)
            {
                //#TODO log
                return BadRequest();
                
            }
            catch (Exception ex)
            {
                //#TODO log
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessToken([FromBody]AccessTokenRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException();
                var result = _authorizationService.RefreshAccessTokenAsync(request);
                return Ok(await result);
            }
            catch (ArgumentException ex)
            {
                //#TODO log
                return BadRequest();

            }
            catch (Exception ex)
            {
                //#TODO log
                throw;
            }
        }

        

    }
}
