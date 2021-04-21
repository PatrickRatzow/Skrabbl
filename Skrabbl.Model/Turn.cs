using System;

namespace Skrabbl.Model
{
    public class Turn
    {
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public string Word { get; set; }
    }
}
