using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Skrabbl.API.Controllers;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using Skrabbl.Model.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skrabbl.API.Test.Controllers
{
    [TestFixture]
    public class GameLobbyControllerSpec
    {
        private List<GameSettingDto> gameSettingList = GameSettingList();

        private static readonly User User = new User
        {
            Id = 500,
            Email = "test@email.dk",
            Password = "Patrick",
            Salt = "2retnut",
            Username = "UserMAN"
        };

        private static readonly GameSettingDto GameSetting = new GameSettingDto
        {
            Setting = "MaxPlayers",
            Value = "4"
        };

        private static List<GameSettingDto> GameSettingList() 
        {
            //GameSetting gameSetting = GameSetting;
            List<GameSettingDto> gameSettingList = new List<GameSettingDto>();
            gameSettingList.Add(GameSetting);
            return gameSettingList;
        }

        private static readonly GameLobby GameLobby = new GameLobby
        {
            GameCode = "hGhG",
           

        };

        private (GameLobbyController, Mock<IUserService>, Mock<IGameLobbyService>) TestObjects()
        {
            var userService = new Mock<IUserService>();
            var gameLobbyService = new Mock<IGameLobbyService>();
            var gameLobbyController = new GameLobbyController(gameLobbyService.Object, userService.Object);

            return (gameLobbyController, userService, gameLobbyService);
        }

        [Test]
        public async Task UserAlreadyHaveLobby()
        {
            //Arrange
            
            var (gameLobbyController, userService, gameLobbyService) = TestObjects();
            userService.Setup(m => m.GetUser(User.Id))
                .ReturnsAsync(() => User);
            gameLobbyService.Setup(m => m.AddGameLobby(It.IsAny<int>(), gameSettingList))
                .ThrowsAsync(new UserAlreadyHaveALobbyException());

            //Act
            var result = await gameLobbyController.Create(gameSettingList, User.Id);

            //Assert
            Assert.IsInstanceOf<ForbidResult>(result);
            userService.VerifyAll();
            gameLobbyService.VerifyAll();
        }

        [Test]
        public async Task CreateLobby()
        {
            //Arrange
            var (gameLobbyController, userService, gameLobbyService) = TestObjects();
            userService.Setup(m => m.GetUser(User.Id))
                .ReturnsAsync(() => User);
            gameLobbyService.Setup(m => m.AddGameLobby(It.IsAny<int>(), gameSettingList))
                .ReturnsAsync(() => GameLobby);

            //Act
            var result = await gameLobbyController.Create(gameSettingList, User.Id);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(GameLobby.GameCode, ((result as OkObjectResult).Value as GameLobby).GameCode);
            userService.VerifyAll();
            gameLobbyService.VerifyAll();
        }

        [Test]
        public async Task UserIsMemberOfALobby()
        {
            //Arrange
            User user = new User
            {
                Id = 500,
                Email = "test@email.dk",
                Password = "Patrick",
                Salt = "2retnut",
                Username = "UserMAN",
                GameLobbyId = "GhhG"
            };
            var (gameLobbyController, userService, _) = TestObjects();
            userService.Setup(m => m.GetUser(user.Id))
                .ReturnsAsync(() => user);

            //Act
            var result = await gameLobbyController.Create(gameSettingList, user.Id);

            //Assert
            Assert.IsInstanceOf<ForbidResult>(result);
            userService.VerifyAll();
        }

        /*public async Task CanUserJoinLobby()
        {

        }
        */
    }
}