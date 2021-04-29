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

        private static readonly LoginDto LoginDto = new LoginDto
        {
            Password = "hunter2",
            Username = "hunter2"
        };

        private static readonly Jwt Jwt = new Jwt
        {
            Token = "nekot",
            ExpiresAt = DateTime.UtcNow.AddDays(2)
        };

        private static readonly RefreshToken RefreshToken = new RefreshToken
        {
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            Token = "nekot",
            User = User
        };

        private static readonly RefreshDto RefreshDto = new RefreshDto
        {
            Token = "nekot"
        };

        private (UserController, Mock<IUserService>, Mock<IJwtService>) TestObjects()
        {
            var userService = new Mock<IUserService>();
            var jwtService = new Mock<IJwtService>();
            var userController = new UserController(userService.Object, jwtService.Object);

            return (userController, userService, jwtService);
        }

        [Test]
        public async Task CreateUserSucceeds()
        {
            // Arrange
            var (controller, userService, _) = TestObjects();
            userService.Setup(m => m.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => User);

            // Act
            var result = await controller.PostUser(UserRegistrationDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            userService.VerifyAll();
        }

        [Test]
        public async Task LoginReturnsUnauthorizedIfIncorrectCredentials()
        {
            // Arrange
            var (controller, userService, _) = TestObjects();
            userService.Setup(m => m.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await controller.Login(LoginDto);

            // Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
            userService.VerifyAll();
        }

        [Test]
        public async Task LoginReturnsOkIfCorrectCredentials()
        {
            // Arrange
            var (controller, userService, jwtService) = TestObjects();
            userService.Setup(m => m.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => User);
            jwtService.Setup(m => m.GenerateSecurityToken(User))
                .Returns(Jwt);
            jwtService.Setup(m => m.GenerateRefreshToken(User))
                .ReturnsAsync(() => RefreshToken);

            // Act
            var result = await controller.Login(LoginDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            userService.VerifyAll();
            jwtService.VerifyAll();
        }

        [Test]
        public async Task RefreshCannotFindMatchingUser()
        {
            // Arrange
            var (controller, userService, _) = TestObjects();
            userService.Setup(m => m.GetUserByRefreshToken(RefreshDto.Token))
                .ReturnsAsync(() => null);

            // Act
            var result = await controller.Refresh(RefreshDto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            userService.VerifyAll();
        }

        [Test]
        public async Task RefreshCannotRefreshToken()
        {
            // Arrange
            var (controller, userService, jwtService) = TestObjects();
            userService.Setup(m => m.GetUserByRefreshToken(RefreshDto.Token))
                .ReturnsAsync(() => User);
            jwtService.Setup(m => m.RefreshToken(User, RefreshDto.Token))
                .ReturnsAsync(() => null);

            // Act
            var result = await controller.Refresh(RefreshDto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            userService.VerifyAll();
            jwtService.VerifyAll();
        }

        [Test]
        public async Task RefreshTokenSuccessfully()
        {
            // Arrange
            var (controller, userService, jwtService) = TestObjects();
            userService.Setup(m => m.GetUserByRefreshToken(RefreshDto.Token))
                .ReturnsAsync(() => User);
            jwtService.Setup(m => m.RefreshToken(User, RefreshDto.Token))
                .ReturnsAsync(() => RefreshToken);

            // Act
            var result = await controller.Refresh(RefreshDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            userService.VerifyAll();
            jwtService.VerifyAll();
        }

        [Test]
        public async Task Logout()
        {
            // Arrange
            var (controller, _, jwtService) = TestObjects();
            jwtService.Setup(m => m.RemoveToken(RefreshToken.Token));

            // Act
            var result = await controller.Logout(RefreshDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            jwtService.VerifyAll();
        }
    }
}