using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.GameClient
{
    public static class DataContainer
    {
        public static Tokens? Tokens { get; set; }

        public static bool IsTokenExpired()
        {
            if (Tokens == null) return false;

            return Tokens.Jwt.ExpiresAt <= DateTime.UtcNow;
        }
    }
}
