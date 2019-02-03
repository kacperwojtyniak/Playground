using AuthService.Requests;
using AuthService.Responses;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LogInResponse> LogInAsync(LogInRequest request);
        Task<AccessToken> RefreshAccessTokenAsync(AccessTokenRequest request);
    }
}
