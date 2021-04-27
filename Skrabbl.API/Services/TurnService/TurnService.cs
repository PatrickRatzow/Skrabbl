using Skrabbl.DataAccess;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Skrabbl.API.Services.TurnService
{
    public class TurnTimer
    {
        public event ElapsedEventHandler TurnOver;
        public event EventHandler<TurnLetterFoundEventArgs> FoundLetter;
        private Timer _turnTimer { get; set; }
        private Timer _letterTimer { get; set; }
        private HashSet<int> _usedIndices = new HashSet<int>();
        private string _currentWord { get; }
        private Func<bool> _shouldSendLetter;

        public Func<bool> ShouldSendLetter
        {
            get => _shouldSendLetter;
            set => _shouldSendLetter = value;
        }

        public TurnTimer(string currentWord, int turnInterval, int letterInterval, Func<bool> shouldSendLetterFunc = null)
        {
            if (shouldSendLetterFunc == null)
            {
                shouldSendLetterFunc = () =>
                {
                    var rnd = new Random();
                    var length = _currentWord.Length;
                    var val = rnd.Next(1, 10);

                    return val == 1;
                };
            }

            ShouldSendLetter = shouldSendLetterFunc;

            _currentWord = currentWord;

            _turnTimer = new Timer();
            _turnTimer.Interval = turnInterval;
            _turnTimer.Elapsed += TurnOver;

            _letterTimer = new Timer();
            _letterTimer.Interval = letterInterval;
            _letterTimer.Elapsed += (sender, eventArgs) =>
            {
                var shouldRun = ShouldSendLetter();

                var possibilties = new List<Tuple<char, int>>();
                for (int i = 0; i < _currentWord.Length; i++)
                {
                    if (_usedIndices.Contains(i)) continue;

                    possibilties.Add(new Tuple<char, int>(_currentWord[i], i));
                }

                var rnd = new Random();
                var chosenTuple = possibilties.OrderBy(x => rnd.Next())
                    .First();

                _usedIndices.Add(chosenTuple.Item2);

                // If we have sent all letters, end the round
                if (_usedIndices.Count == _currentWord.Length)
                {
                    TurnOver(sender, eventArgs);

                    return;
                }

                FoundLetter(sender, new TurnLetterFoundEventArgs
                {
                    Index = chosenTuple.Item2,
                    Letter = chosenTuple.Item1
                });
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

        public void RemoveTurnTimer(int gameId)
        {
            _timers.TryRemove(gameId, out var timer);
        }

        public void DetermineChance(int gameId, int userId)
        {
            //Based of the length of a word we want to send a letter. 
            bool success = _timers.TryGetValue(gameId, out var timer);
            var word = CurrentWord(userId);
            if (!success)
                return;

            timer.Start();
        }

        public async Task<string> CurrentWord(int userId)
        {
            var turn = await _gameRepository.GetCurrentTurn(userId);
            return turn.Word;
        }
    }
}