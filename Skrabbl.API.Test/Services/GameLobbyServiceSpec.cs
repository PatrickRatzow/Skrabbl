using NUnit.Framework;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Skrabbl.API.Services;
using Skrabbl.Model.Errors;
using NUnit.Framework.Constraints;

namespace Skrabbl.API.Test.Services
{
    class GameLobbyServiceSpec
    {
        [Test]
        public async Task AddGameLobby_Succeeds_WhenUserIsNotInAnyLobby()
        {
            //Arrange
            int userId = 25;
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.AddGameLobby(It.IsAny<GameLobby>()));
            mock.Setup(m => m.GetLobbyByOwnerId(userId)).Returns(null);
            mock.Setup(m => m.GetGameLobbyById(It.IsAny<string>()));
            var service = new GameLobbyService(mock.Object);

            //Act
            await service.AddGameLobby(userId);

            //Assert
            mock.VerifyAll();
        }

        [Test]
        public async Task AddGameLobby_ThrowsAUserInLobbyException_WhenUserIsInLobby()
        {
            //Arrange
            int userId = 25;
            var gameLobby = new GameLobby();

            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetLobbyByOwnerId(userId)).Returns(new ValueTask<GameLobby>(gameLobby));
            var service = new GameLobbyService(mock.Object);

            //Act
            var ex = Assert.ThrowsAsync<UserAlreadyHaveALobbyException>(
                async () => await service.AddGameLobby(userId));


            //Assert            
            Assert.IsNotNull(ex);
            mock.VerifyAll();
        }
    }
}
