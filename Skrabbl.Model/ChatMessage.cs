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
        public User User { get; set; }
        public Game Game { get; set; }
        
    }
}
