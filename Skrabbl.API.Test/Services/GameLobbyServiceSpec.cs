using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Skrabbl.API.Services;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using Skrabbl.Model.Errors;

namespace Skrabbl.API.Test.Services
{
    class GameLobbyServiceSpec
    {
        private List<GameSettingDto> gameSettingList = new List<GameSettingDto>();


        private static readonly GameSettingDto GameSetting = new GameSettingDto
        {
            Setting = "MaxPlayers",
            Value = "4"
        };

        [Test]
        public async Task AddGameLobby_Succeeds_WhenUserIsNotInAnyLobby()
        {
            //Arrange
            int userId = 25;
            gameSettingList.Add(GameSetting);


            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.AddGameLobby(It.IsAny<GameLobby>()));
            mock.Setup(m => m.GetLobbyByOwnerId(userId))
                .ReturnsAsync(() => null);
            mock.Setup(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()));
            var service = new GameLobbyService(mock.Object);

            //Act
            await service.AddGameLobby(userId, gameSettingList);

            //Assert
            mock.VerifyAll();
        }

        [Test]
        public void AddGameLobby_ThrowsAUserInLobbyException_WhenUserIsInLobby()
        {
            //Arrange
            int userId = 25;
            var gameLobby = new GameLobby();

            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetLobbyByOwnerId(userId))
                .ReturnsAsync(() => gameLobby);
            var service = new GameLobbyService(mock.Object);

            //Act
            var ex = Assert.ThrowsAsync<UserAlreadyHaveALobbyException>(
                async () => await service.AddGameLobby(userId, gameSettingList));

            //Assert            
            Assert.IsNotNull(ex);
            mock.Verify(m => m.GetLobbyByOwnerId(userId), Times.Once);
        }

        [Test]
        public async Task AddGameLobby_GenerateLobbyCodeTwice_WhenTheFirstGameCodeHasALobby()
        {
            //Arrange
            int userId = 25;
            var gameLobby = new GameLobby();
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.AddGameLobby(It.IsAny<GameLobby>()));
            mock.Setup(m => m.GetLobbyByOwnerId(userId))
                .ReturnsAsync(() => null);
            mock.SetupSequence(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()))
                .ReturnsAsync(() => gameLobby)
                .ReturnsAsync(() => null);

            var service = new GameLobbyService(mock.Object);

            //Act
            await service.AddGameLobby(userId, gameSettingList);

            //Assert
            mock.Verify(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()), Times.Exactly(2));
            mock.VerifyAll();
        }

        [Test]
        public async Task RemoveGameLobby_Succeeds_RemovesExistingGameLobby()
        {
            var gameLobby = new GameLobby();
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()))
                .ReturnsAsync(() => gameLobby);
            var service = new GameLobbyService(mock.Object);

            var success = await service.RemoveGameLobby("");

            Assert.IsTrue(success);
            mock.Verify(m => m.RemoveGameLobby(It.IsAny<string>()), Times.Once());
            mock.VerifyAll();
        }

        [Test]
        public async Task RemoveGameLobby_DoesntRemoveAnyLobby_IfLobbyDoesntExists()
        {
            var gameLobby = new GameLobby();
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()))
                .ReturnsAsync(() => null);
            var service = new GameLobbyService(mock.Object);

            var success = await service.RemoveGameLobby("");

            Assert.IsFalse(success);
            mock.Verify(m => m.RemoveGameLobby(It.IsAny<string>()), Times.Never());
            mock.VerifyAll();
        }

        [Test]
        public async Task GetGameLobbyById_ChecksIfCallsRepository()
        {
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()));
            var service = new GameLobbyService(mock.Object);

            await service.GetGameLobbyById("");

            mock.Verify(m => m.GetGameLobbyByLobbyCode(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task GetAllGameLobbies_ChecksIfCallsRepository()
        {
            var mock = new Mock<IGameLobbyRepository>();
            var service = new GameLobbyService(mock.Object);

            await service.GetAllGameLobbies();

            mock.Verify(m => m.GetAllGameLobbies(), Times.Once());
        }

        [Test]
        public async Task GetGameLobbyByOwnerId_ChecksIfCallsRepository()
        {
            var mock = new Mock<IGameLobbyRepository>();
            mock.Setup(m => m.GetLobbyByOwnerId(It.IsAny<int>()));
            var service = new GameLobbyService(mock.Object);

            await service.GetLobbyByOwnerId(0);

            mock.Verify(m => m.GetLobbyByOwnerId(It.IsAny<int>()), Times.Once());
        }
    }
}