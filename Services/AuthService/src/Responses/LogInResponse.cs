using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Responses
{
    public class LogInResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
