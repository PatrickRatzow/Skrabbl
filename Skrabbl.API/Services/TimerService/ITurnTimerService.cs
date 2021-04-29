using System;
using System.Timers;

namespace Skrabbl.API.Services.TimerService
{
    public interface ITurnTimerService
    {
        void CreateTimer(int gameId, int interval, Action<Timer> callback = null);
        void StartTimer(int gameId);
        void StopTimer(int gameId);
        Timer GetTimer(int gameId);
        void DestroyTimer(int gameId);
    }
}