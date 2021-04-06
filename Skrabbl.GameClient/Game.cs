using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.GameClient
{
    class Game
    {
        private int maxPlayers;
        private int rounds;
        private int drawingTime;

        public Game(int maxPlayers, int rounds, int drawingTime)
        {
            this.maxPlayers = maxPlayers;
            this.rounds = rounds;
            this.drawingTime = drawingTime;
        }
    }
}
