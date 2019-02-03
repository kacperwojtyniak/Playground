using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Configurations
{
    public class TokensConfiguration
    {
        public int AccessTokenExpMinutes { get; set; }
        public int RefreshTokenExpHours { get; set; }        
    }
}
