using System.Collections.Generic;

namespace Skrabbl.Model
{
    public class Round
    {
        public int Id { get; set; }
        public int RoundNumber { get; set; }
        public int GameId { get; set; }
        public List<Turn> Turns { get; set; } = new List<Turn>();
    }
}