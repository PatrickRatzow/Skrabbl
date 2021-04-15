using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class Turn
    {
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public GuessWord Word { get; set; }
    }
}
