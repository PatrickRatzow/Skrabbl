using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model
{
    public class GuessWord
    {
        public string Word { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GuessWord word &&
                   Word == word.Word;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Word);
        }
    }
}
