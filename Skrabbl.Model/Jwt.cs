using System;

namespace Skrabbl.Model
{
    [Serializable]
    public class Jwt
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}