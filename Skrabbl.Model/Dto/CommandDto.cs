using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Skrabbl.Model.Dto
{


    [Serializable]
    public class CommandDto
    {
        public IList<UInt16>? StartNode { get; set; }
        public IList<UInt16>? ContinueNode { get; set; }
        public IList<byte>? Color { get; set; }
        public byte? Thickness { get; set; }
    }
}
