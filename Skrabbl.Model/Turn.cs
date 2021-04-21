using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class Turn
    {
        public DateTime EndTime { get; set; } //End of the timer??
        public DateTime StartTime { get; set; } // Start of the timer??
        public GuessWord Word { get; set; }

    }
}
