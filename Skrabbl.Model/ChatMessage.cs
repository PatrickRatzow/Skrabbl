using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public User user { get; }
        public Game game { get; }
    }
}
