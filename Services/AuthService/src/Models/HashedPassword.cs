using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class HashedPassword
    {
        public string PasswordHash { get; set; }
        public string SaltBase64 { get; set; }
    }
}
