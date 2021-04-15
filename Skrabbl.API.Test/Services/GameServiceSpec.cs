using System;
using NUnit.Framework;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System.Threading.Tasks;
using Moq;
using Skrabbl.API.Services;
using Skrabbl.Model.Errors;
using NUnit.Framework.Constraints;


namespace Skrabbl.API.Test.Services
{
    class GameServiceSpec
    {
        [Test]
        public async Task GetGame_Succeeds()
        {
            //Arrange
            int gameiD = 3;
            var mock = new Mock<GameRepository>();
            mock.Setup(m => m.GetGame(It.IsAny<int>())).Returns(Task.FromResult<Game>(null));
            var service = new GameService(mock.Object);

            //Act
            await service.GetGame(gameiD);

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
