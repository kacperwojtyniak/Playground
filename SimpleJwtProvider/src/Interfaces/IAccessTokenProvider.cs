using SimpleJwtProvider.Models;
using System;
using System.Collections.Generic;

namespace SimpleJwtProvider.Interfaces
{
    public interface IAccessTokenProvider
    {
        AccessToken GetAccessToken(DateTime expDate, Dictionary<string, object> claims);        
    }
}
