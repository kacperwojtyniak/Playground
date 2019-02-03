using AuthService.Requests;
using AuthService.Responses;
using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Interfaces
{
    public interface ITokenService
    {
        Task<AccessToken> GetAccessTokenAsync(AccessTokenRequest request);
        Task<LogInResponse> LogInAsync(string userRole);
    }
}
