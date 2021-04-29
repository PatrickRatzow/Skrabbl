using System;
using System.Collections.Generic;

namespace Skrabbl.Model
{
    public class Turn
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Word { get; set; }

        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}