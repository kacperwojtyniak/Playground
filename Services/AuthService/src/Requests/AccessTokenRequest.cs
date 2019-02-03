using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Requests
{
    public class AccessTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
