using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Skrabbl.API.Controllers;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Errors;
using System.Threading.Tasks;

namespace Skrabbl.API.Test.Controllers
{
    [TestFixture]
    public class GameLobbyControllerSpec
    {
        private static readonly User User = new User
        {
            Id = 500,
            Email = "test@email.dk",
            Password = "Patrick",
            Salt = "2retnut",
            Username = "UserMAN"
        };

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
            gameLobbyService.Setup(m => m.AddGameLobby(It.IsAny<int>()))
                .ThrowsAsync(new UserAlreadyHaveALobbyException());

            //Act
            var result = await gameLobbyController.Create(User.Id);

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
            gameLobbyService.Setup(m => m.AddGameLobby(It.IsAny<int>()))
                .ReturnsAsync(() => GameLobby);

            //Act
            var result = await gameLobbyController.Create(User.Id);

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
            var result = await gameLobbyController.Create(user.Id);

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