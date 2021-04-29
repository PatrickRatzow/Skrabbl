using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Skrabbl.API.Services;
using Skrabbl.API.Services.TimerService;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Test.Services
{
    class GameServiceSpec
    {
        [Test]
        public async Task GetGame_Succeeds()
        {
            //Arrange
            int gameId = 5;
            Game game = new Game();
            var mock = new Mock<IGameRepository>();
            mock.Setup(m => m.GetGame(It.IsAny<int>())).Returns(Task.FromResult<Game>(game));
            var service = new GameService(mock.Object, null);

            //Act
            await service.GetGame(gameId);

            //Assert
            mock.VerifyAll();
        }

        //Has not been implemented yet :)
        //[Test]
        //public Task EndTurn_Succeeds()
        //{
        //    //Arrange
        //    var mock = new Mock<GameRepository>();
        //    var service = new GameService(mock.Object);
        //    //Act
        //    service.EndTurn();
        //    //Assert
        //    mock.VerifyAll();
        //}
    }
}
