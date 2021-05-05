using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Skrabbl.DataAccess;

namespace Skrabbl.API.Services.TurnService
{
    public class TurnTimer
    {
        public event ElapsedEventHandler TurnOver;
        public event EventHandler<TurnLetterFoundEventArgs> FoundLetter;
        public Func<bool> ShouldSendLetter { private get; set; }
        private Timer _turnTimer { get; set; }
        private Timer _letterTimer { get; set; }
        private HashSet<int> _usedIndices = new HashSet<int>();
        private string _currentWord { get; }

        public TurnTimer(string currentWord, int turnInterval, int letterInterval)
        {
            ShouldSendLetter ??= DefaultShouldSendLetter;

            _currentWord = currentWord;

            _turnTimer = new Timer();
            _turnTimer.Interval = turnInterval;
            _turnTimer.Elapsed += TurnOver;

            _letterTimer = new Timer();
            _letterTimer.Interval = letterInterval;

            _letterTimer.Elapsed += (sender, eventArgs) =>
            {
                var shouldRun = ShouldSendLetter();

                var possibilties = new List<(char, int)>();
                for (int i = 0; i < _currentWord.Length; i++)
                {
                    if (_usedIndices.Contains(i)) continue;

                    possibilties.Add((_currentWord[i], i));
                }

                var rnd = new Random();
                var (letter, index) = possibilties.OrderBy(x => rnd.Next())
                    .First();

                _usedIndices.Add(index);

                FoundLetter?.Invoke(sender, new TurnLetterFoundEventArgs
                {
                    Index = index,
                    Letter = letter
                });

                // If we have sent all letters, end the round
                if (_usedIndices.Count == _currentWord.Length)
                {
                    TurnOver?.Invoke(sender, eventArgs);
                }
            };
        }

        public void Start()
        {
            _turnTimer.Start();
            _letterTimer.Start();
        }

        public void Stop()
        {
            _turnTimer.Stop();
            _letterTimer.Stop();
        }

        private bool DefaultShouldSendLetter()
        {
            var rnd = new Random();
            var length = _currentWord.Length;
            var val = rnd.Next(1, 10);

            return val == 1;
        }
    }

    public class TurnLetterFoundEventArgs : EventArgs
    {
        public int Index { get; set; }
        public char Letter { get; set; }
    }

    public class TurnService : ITurnService
    {
        private readonly ConcurrentDictionary<int, TurnTimer> _timers;
        private readonly IGameRepository _gameRepository;

        public TurnService(IGameRepository gameRepository)
        {
            _timers = new ConcurrentDictionary<int, TurnTimer>();
            _gameRepository = gameRepository;
        }

        public TurnTimer CreateTurnTimer(int gameId, string currentWord, int turnInterval, int letterInterval)
        {
            TurnTimer timer = new TurnTimer(currentWord, turnInterval, letterInterval);

            _timers.TryAdd(gameId, timer);

            return timer;
        }

        public TurnTimer GetTurnTimer(int gameId)
        {
            bool success = _timers.TryGetValue(gameId, out var timer);
            if (!success)
                return null;

            return timer;
        }

        public bool RemoveTurnTimer(int gameId)
        {
            return _timers.TryRemove(gameId, out var timer);
        }
    }
}