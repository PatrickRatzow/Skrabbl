using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    [Serializable]
    public class Jwt
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
