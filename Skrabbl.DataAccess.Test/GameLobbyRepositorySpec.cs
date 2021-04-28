using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [TestFixture]
    class GameLobbyRepositorySpec
    {
        IGameLobbyRepository _gameLobbyRepository;

        [SetUp]
        public void Setup()
        {
            var cmd = new CommandText();

            _gameLobbyRepository = new GameLobbyRepository(ConfigFixture.Config, cmd);

            TestData.GameLobbies.FlorisLobby = new GameLobby()
            {
                GameCode = "a1b1",
                LobbyOwnerId = TestData.Users.Floris.Id,
            };
        }

        [Test, Order(1)]
        public async Task AddGameLobbyToDb()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            await _gameLobbyRepository.AddGameLobby(gameLobby);
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyById(gameLobby.GameCode);

            //Assert
            Assert.IsNotNull(lobby);
        }

        [Test, Order(2)]
        public async Task FindGameLobbyByOwnerId()
        {
            //Arrange
            int userId = TestData.Users.Floris.Id;

            //Act
            var gameLobby = await _gameLobbyRepository.GetLobbyByOwnerId(userId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test, Order(3)]
        public async Task FindGameLobbyById()
        {
            //Arrange
            string lobbyId = TestData.GameLobbies.FlorisLobby!.GameCode;

            //Act
            var gameLobby = await _gameLobbyRepository.GetGameLobbyById(lobbyId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test, Order(4)]
        public async Task RemoveGameLobby()
        {
            //Arrange
            string lobbyId = TestData.GameLobbies.FlorisLobby!.GameCode;

            //Act
            int rowsAffected = await _gameLobbyRepository.RemoveGameLobby(lobbyId);
            TestData.GameLobbies.FlorisLobby = null;

            //Assert
            Assert.AreEqual(rowsAffected, 1);
        }

        [Test, Order(5)]
        public async Task GetAllLobbies()
        {
            //Arrange

            //Act
            var lobbies = await _gameLobbyRepository.GetAllGameLobbies();

            //Assert
            // According to seeding data we have 2 lobbies
            Assert.AreEqual(lobbies.Count(), 2);
        }
    }
}