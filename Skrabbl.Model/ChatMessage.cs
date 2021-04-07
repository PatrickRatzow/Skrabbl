using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public User Player { get; set; }
        public Game CurrentGame { get; set; }
        
    }
}
