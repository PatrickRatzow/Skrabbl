using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Skrabbl.API.Services;
using Moq;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Test.Services
{
    class UserServiceSpec
    {
        [Test]
        public async Task CreateUser_Succeeds_WithLegalUsernameAndPassWord()
        {
            //Arrange
            int expectedId = 1;
            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            cryptographyMock.Setup(m => m.CreateSalt()).Returns(new byte[16]);
            cryptographyMock.Setup(m => m.GenerateHash(It.IsAny<string>(), It.IsAny<Byte[]>())).Returns("");
            userRepoMock.Setup(m => m.AddUser(It.IsAny<User>())).Returns(Task.FromResult(expectedId));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = await userService.CreateUser("", "", "");

            //Assert
            Assert.AreEqual(user.Id, expectedId);
            cryptographyMock.VerifyAll();
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task GetUser_Succeeds_WithlegalUsernameAndPassWord()
        {
            //Arrange

            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            cryptographyMock.Setup(m => m.AreEqual(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Byte[]>())).Returns(true);
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = userService.GetUser("Bob", "Alice");

            //Assert
            Assert.IsNotNull(user);
            cryptographyMock.VerifyAll();
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task GetUser_Fails_WithExistingUsernameAndNonexistingPassWord()
        {
            //Arrange
            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";

            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            cryptographyMock.Setup(m => m.AreEqual(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Byte[]>())).Returns(false);
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = await userService.GetUser("Bob", "Alice");

            //Assert
            Assert.IsNull(user);
            cryptographyMock.VerifyAll();
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task GetUser_Fails_WithNonexistingUsername()
        {
            //Arrange
            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = await userService.GetUser("Bob", "Alice");

            //Assert
            Assert.IsNull(user);
            cryptographyMock.VerifyAll();
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task GetUser_Succeds_WithExistingUserId()
        {
            //Arrange
            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            expectedUser.Id = 3;

            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = await userService.GetUser(expectedUser.Id);

            //Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Id, expectedUser.Id);
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task GetUser_Succeds_WithNonexistingUserId()
        {
            //Arrange
            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            expectedUser.Id = 3;

            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(Task.FromResult<User>(null));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            var user = await userService.GetUser(expectedUser.Id);

            //Assert
            Assert.IsNull(user);
            userRepoMock.VerifyAll();

        }

        [Test]
        public async Task AddToLobby_Succeds_RunsAddUserToLobby()
        {
            //Arrange
            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            expectedUser.Id = 3;

            var cryptographyMock = new Mock<ICryptographyService>();
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(m => m.AddUserToLobby(It.IsAny<int>(), It.IsAny<string>()));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object);

            //Act
            await userService.AddToLobby(expectedUser.Id, "");

            //Assert
            userRepoMock.VerifyAll();

        }
    }
}
