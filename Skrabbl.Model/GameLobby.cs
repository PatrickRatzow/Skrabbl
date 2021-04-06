using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class GameLobby
    {
        public string GameCode { get; set; }
        public int UserOwnerId { get; set; }
        public User User { get; set; }
    }
}
