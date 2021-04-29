using System;
using System.Timers;

namespace Skrabbl.API.Services.TurnService
{
    public interface ITurnService
    {
        TurnTimer CreateTurnTimer(int gameId, string currentWord, int turnInterval, int letterInterval);
        TurnTimer GetTurnTimer(int gameId);
        void RemoveTurnTimer(int gameId);
    }
}