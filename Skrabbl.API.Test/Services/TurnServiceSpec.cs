using Moq;
using NUnit.Framework;
using Skrabbl.API.Services;
using Skrabbl.API.Services.TurnService;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.API.Test.Services
{
    class TurnServiceSpec
    {
        private int gameId = 5;
        private Game game = new Game();
        private int turnInterval = 500;
        private int letterInterval = 10;
        private string currentWord = "Cake";

        [Test]
        public async Task TimerElapsed_Success()
        {
            //Arrange
            var service = new TurnService(null);
            var timer = service.CreateTurnTimer(gameId, currentWord, turnInterval, letterInterval);
            bool hasElapsed = false;

            //Act
            timer.TurnOver += (s, e) => hasElapsed = true;
            timer.Start();
            await Task.Delay(600);

            //Assert
            Assert.IsTrue(hasElapsed);
        }

        [Test]
        public async Task FoundALetterEvent_Success()
        {
            var service = new TurnService(null);
            var timer = service.CreateTurnTimer(gameId, currentWord, turnInterval, letterInterval);
            char foundLetter = 'å';

            //Act
            timer.ShouldSendLetter = () => true;
            timer.FoundLetter += (s, e) => foundLetter = e.Letter;
            timer.Start();
            await Task.Delay(letterInterval + 5);

            //Assert
            Assert.AreNotEqual(foundLetter, 'å');
        }
    }
}
