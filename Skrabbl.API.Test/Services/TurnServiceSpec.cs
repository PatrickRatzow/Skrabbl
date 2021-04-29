using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.API.Services.TurnService;

namespace Skrabbl.API.Test.Services
{
    class TurnServiceSpec
    {
        private int _turnInterval = 500;
        private int _letterInterval = 10;

        private TurnTimer Timer(int gameId = 5, string word = "Cake", int turnInterval = 0, int letterInterval = 0)
        {
            turnInterval = turnInterval <= 0 ? _turnInterval : turnInterval;
            letterInterval = letterInterval <= 0 ? _letterInterval : letterInterval;

            var service = new TurnService(null);
            return service.CreateTurnTimer(gameId, word, turnInterval, letterInterval);
        }

        [Test]
        public async Task TimerElapsed_Success()
        {
            //Arrange
            var timer = Timer();
            var cts = new CancellationTokenSource();
            bool hasElapsed = false;

            //Act
            timer.TurnOver += (s, e) =>
            {
                hasElapsed = true;

                cts.Cancel();
            };
            timer.Start();
            try
            {
                await Task.Delay(_turnInterval + 250, cts.Token);
            }
            catch
            {
                // Ignored
            }

            timer.Stop();

            //Assert
            Assert.IsTrue(hasElapsed);
        }

        [Test]
        public async Task FoundALetterEvent_Success()
        {
            var timer = Timer();
            var cts = new CancellationTokenSource();
            char foundLetter = 'å';

            //Act
            timer.ShouldSendLetter = () => true;
            timer.FoundLetter += (s, e) =>
            {
                foundLetter = e.Letter;

                cts.Cancel();
            };
            timer.Start();
            try
            {
                await Task.Delay(_letterInterval + 250, cts.Token);
            }
            catch
            {
                // Ignored
            }

            timer.Stop();

            // Assert
            Assert.AreNotEqual(foundLetter, 'å');
        }

        [Test]
        public async Task AllLettersHaveBeenIndentifiedAndTurnOverHasBeenCalled()
        {
            //Arrange 
            string longerWord = "fpewpgfowegoprejmgpiwemjpigwmi";
            var timer = Timer(default, longerWord);
            bool turnIsOver = false;
            var cts = new CancellationTokenSource();
            char[] foundLetter = new char[longerWord.Length];

            //Act
            timer.ShouldSendLetter = () => true;
            timer.FoundLetter += (o, e) => { foundLetter[e.Index] = e.Letter; };
            timer.TurnOver += (o, e) =>
            {
                turnIsOver = true;
                cts.Cancel();
            };

            timer.Start();
            try
            {
                await Task.Delay(_turnInterval + 250, cts.Token);
            }
            catch
            {
                // Ignored
            }

            timer.Stop();

            //Assert
            //Hack to remove null terminators
            var result = new string(foundLetter).Replace("\0", "") + "";
            Assert.AreEqual(result, longerWord);
            Assert.True(turnIsOver);
        }
    }
}