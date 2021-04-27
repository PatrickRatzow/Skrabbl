using System.Collections.Generic;

namespace Skrabbl.Model
{
    public class Game
    {
        public int Id { get; set; }
        public int ActiveRoundId { get; set; }
        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}