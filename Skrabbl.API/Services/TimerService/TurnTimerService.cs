using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Skrabbl.API.Services.TimerService
{
    public class TurnTimerService
    {
        private readonly ConcurrentDictionary<int, Timer> _timers;

        public TurnTimerService()
        {
            _timers = new ConcurrentDictionary<int, Timer>();
        }

        public void CreateTimer(int gameId, int interval)
        {
            Timer timer = new Timer();
            timer.Elapsed += TimerElapsed;
            timer.Interval = interval;
            _timers.TryAdd(gameId, timer);
        }
        //This happens when the Timer reached 0, and tries to start over.
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //Stop the turn, and then the timer?
            //Send message forward
            var timer = sender as Timer;
            timer.Stop();
        }

        public void StartTimer(int gameId)
        {
            bool success = _timers.TryGetValue(gameId, out Timer timer);
            if (!success) 
                return;

            timer.Start();
        }

        public void StopTimer(int gameId)
        {
            bool success = _timers.TryGetValue(gameId, out Timer timer);
            if (!success)
                return;

            timer.Stop();
        }

        public Timer GetTimer(int gameId)
        {
            bool success = _timers.TryGetValue(gameId, out Timer timer);
            if (!success)
                return null;

            return timer;
        }

        public void DestroyTimer(int gameId)
        {
            bool success = _timers.TryRemove(gameId, out Timer timer);
            if (!success)
                return;

            timer.Dispose();
        }
    }
}
