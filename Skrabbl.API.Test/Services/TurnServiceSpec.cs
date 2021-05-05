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
        private TurnService service;
        private int userId = 5;
        
        //20% of this one is not covered.
        private TurnTimer Timer(int gameId = 5, string word = "Cake", int turnInterval = 0, int letterInterval = 0)
        {
            //Tests are not covered 100% because we never hit these two fields.
            turnInterval = turnInterval <= 0 ? _turnInterval : turnInterval;
            letterInterval = letterInterval <= 0 ? _letterInterval : letterInterval;

            service = new TurnService(null);
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

        [Test]
        public async Task ShouldSendLetterWorks()
        {
            //Arrange
            var timer = Timer();
            bool hasSendLetter = false;
            var cts = new CancellationTokenSource();

            //Act
            timer.Start();
            timer.ShouldSendLetter = () => hasSendLetter = true;
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
            Assert.True(hasSendLetter);
        }

        [Test]
        public void GetTurnTimer_Success()
        {
            //Arrange 
            var timer = Timer();

            //Act
            timer.Start();
            var getTimer = service.GetTurnTimer(5);

            //Assert
            System.Console.WriteLine(getTimer);
            Assert.True(getTimer != null);
        }

        [Test]
        public void GetTurnTimer_Fails()
        {
            //Arrange 
            var timer = Timer();

            //Act
            timer.Start();
            var getTimer = service.GetTurnTimer(6);

            //Assert
            System.Console.WriteLine(getTimer);
            Assert.True(getTimer == null);
        }

        [Test]
        public void RemoveTurnTimer_Success()
        {
            //Arrange 
            var timer = Timer();

            //Act
            var removedTimer = service.RemoveTurnTimer(5);

            //Assert
            Assert.True(removedTimer);
        }
    }
}