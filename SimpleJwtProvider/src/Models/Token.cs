using System;

namespace SimpleJwtProvider.Models
{
    public abstract class Token
    {
        public string TokenValue { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
