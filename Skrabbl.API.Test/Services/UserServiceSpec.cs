using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Skrabbl.API.Services;
using Moq;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using Skrabbl.Model.Errors;

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            cryptographyMock.Setup(m => m.CreateSalt()).Returns(new byte[16]);
            cryptographyMock.Setup(m => m.GenerateHash(It.IsAny<string>(), It.IsAny<Byte[]>())).Returns("");
            userRepoMock.Setup(m => m.AddUser(It.IsAny<User>())).Returns(Task.FromResult(expectedId));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            cryptographyMock.Setup(m => m.AreEqual(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Byte[]>())).Returns(true);
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            cryptographyMock.Setup(m => m.AreEqual(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Byte[]>())).Returns(false);
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            userRepoMock.Setup(m => m.GetUserByUsername(It.IsAny<string>())).Returns(Task.FromResult<User>(null));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            userRepoMock.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(Task.FromResult<User>(expectedUser));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            userRepoMock.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(Task.FromResult<User>(null));
            var userService = new UserService(userRepoMock.Object, cryptographyMock.Object, gameLobbyMock.Object);

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
            var gameCode = "Asd2";

            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            expectedUser.Id = 3;

            List<User> users = new List<User>();
            users.Add(expectedUser);
            users.Add(expectedUser);
            users.Add(expectedUser);

            GameSetting gameSettingMaxPlayers = new GameSetting {
                SettingType = "MaxPlayers",
                Value = "4"
            };

            GameSetting gameSettingNoOfRounds = new GameSetting
            {
                SettingType = "NoOfRounds",
                Value = "3"
            };

            List<GameSetting> gameSettings = new List<GameSetting>();
            gameSettings.Add(gameSettingMaxPlayers);
            gameSettings.Add(gameSettingNoOfRounds);


            var userRepoMock = new Mock<IUserRepository>();
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            userRepoMock.Setup(m => m.AddUserToLobby(expectedUser.Id, gameCode));
            userRepoMock.Setup(m => m.GetUsersByGameCode(gameCode)).Returns(Task.FromResult<IEnumerable<User>>(users));
            gameLobbyMock.Setup(s => s.GetGameSettingsByGameCode(gameCode)).Returns(Task.FromResult<IEnumerable<GameSetting>>(gameSettings));
            var userService = new UserService(userRepoMock.Object, null, gameLobbyMock.Object);

            //Act
            await userService.AddToLobby(expectedUser.Id, gameCode);

            //Assert
            gameLobbyMock.VerifyAll();
            userRepoMock.VerifyAll();
        }

        [Test]
        public async Task AddToLobby_FailsOnTooManyPlayers()
        {
            //Arrange
            var gameCode = "Asd2";

            var expectedUser = new User();
            expectedUser.Email = "bob@alice.com";
            expectedUser.Password = "";
            expectedUser.Salt = "asdfghjk";
            expectedUser.Id = 3;

            List<User> users = new List<User>();
            users.Add(expectedUser);
            users.Add(expectedUser);
            users.Add(expectedUser);

            GameSetting gameSettingMaxPlayers = new GameSetting
            {
                SettingType = "MaxPlayers",
                Value = "3"
            };

            GameSetting gameSettingNoOfRounds = new GameSetting
            {
                SettingType = "NoOfRounds",
                Value = "4"
            };

            List<GameSetting> gameSettings = new List<GameSetting>();
            gameSettings.Add(gameSettingMaxPlayers);
            gameSettings.Add(gameSettingNoOfRounds);


            var userRepoMock = new Mock<IUserRepository>();
            var gameLobbyMock = new Mock<IGameLobbyRepository>();
            userRepoMock.Setup(m => m.GetUsersByGameCode(gameCode)).Returns(Task.FromResult<IEnumerable<User>>(users));
            gameLobbyMock.Setup(s => s.GetGameSettingsByGameCode(gameCode)).Returns(Task.FromResult<IEnumerable<GameSetting>>(gameSettings));
            var userService = new UserService(userRepoMock.Object, null, gameLobbyMock.Object);

            //Act
            var ex = Assert.ThrowsAsync<LobbyIsFullException>(
                async () => await userService.AddToLobby(expectedUser.Id, gameCode));

            //Assert
            Assert.IsNotNull(ex);
            gameLobbyMock.VerifyAll();
            userRepoMock.VerifyAll();
        }

    }
}
