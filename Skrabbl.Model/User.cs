﻿using System;

namespace Skrabbl.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string GameLobbyId { get; set; }
    }
}
