using NUnit.Framework;
using Skrabbl.API.Services.TimerService;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.API.Test.Services
{
    class TurnTimerServiceSpec
    {
        [Test]
        public async Task StartTimer_Success()
        {
            //Arrange
            int gameId = 5;
            Game game = new Game();
            int time = 60000;

            var service = new TurnTimerService();
            service.CreateTimer(gameId, time);

            //Act
            service.StartTimer(gameId);

            //Assert
            Assert.IsTrue(service.GetTimer(gameId).Enabled);
        }

        [Test]
        public async Task StopTimer_Success()
        {
            //Arrange
            int gameId = 5;
            Game game = new Game();
            int time = 60000;

            var service = new TurnTimerService();
            service.CreateTimer(gameId, time);

            //Act
            service.StopTimer(gameId);

            //Assert
            Assert.IsFalse(service.GetTimer(gameId).Enabled);
        }
        [Test]
        public async Task TimerStoppedMessgae()
        {
            //Arrange
            int gameId = 5;
            Game game = new Game();
            int time = 60000;

            var service = new TurnTimerService();
            service.CreateTimer(gameId, time);

            //Act

            //Assert
            Assert.IsFalse(service.GetTimer(gameId).Enabled);
        }
    }
}
