using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Skrabbl.API.Controllers;
using Skrabbl.API.Services;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

namespace Skrabbl.API.Test.Controllers
{
    [TestFixture]
    public class UserControllerSpec
    {
        private static readonly User User = new User
        {
            Id = 1,
            Email = "test@email.dk",
            Password = "hunter2",
            Salt = "2retnut",
            Username = "nameuser"
        };

        private static readonly UserRegistrationDto UserRegistrationDto = new UserRegistrationDto
        {
            Email = User.Email,
            Password = User.Password,
            UserName = User.Username
        };

        private Tuple<UserController, Mock<IUserService>, Mock<IJwtService>> TestObjects()
        {
            var userService = new Mock<IUserService>();
            var jwtService = new Mock<IJwtService>();
            var userController = new UserController(userService.Object, jwtService.Object);

            return new Tuple<UserController, Mock<IUserService>, Mock<IJwtService>>(userController, userService,
                jwtService);
        }

        [Test]
        public async Task CreateUserSucceeds()
        {
            // Arrange
            var (controller, userService, _) = TestObjects();
            userService.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => User);

            // Act
            var result = await controller.PostUser(UserRegistrationDto) as OkObjectResult;

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreSame(result?.Value, User);
        }
    }
}