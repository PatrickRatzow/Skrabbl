using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;
using System.Linq;

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

            GameSetting gameSetting = new GameSetting()
            {
                GameCode = "a1b1",
                Setting = "MaxPlayers",
                Value = "4"
            };
            GameSetting gameSetting1 = new GameSetting()
            {
                GameCode = "a1b1",
                Setting = "NoOfRounds",
                Value = "10"
            };

            List<GameSetting> gameSettings = new List<GameSetting>();
            gameSettings.Add(gameSetting);
            gameSettings.Add(gameSetting1);

            TestData.GameLobbies.FlorisLobby = new GameLobby()
            {
                GameCode = "a1b1",
                LobbyOwnerId = TestData.Users.Floris.Id,
                GameSettings = gameSettings
            };
        }

        [Test, Order(1)]
        public async Task AddGameLobbyToDb()
        {
            //Arrange

            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            await _gameLobbyRepository.AddGameLobby(gameLobby);

            //Assert
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(gameLobby.GameCode);
            IEnumerable<GameSetting> gameSetting = await _gameLobbyRepository.GetGameSettingsByGameCode(gameLobby.GameCode);
            List<GameSetting> gameSettingList = gameSetting.ToList();

            Assert.IsNotNull(lobby);
            Assert.AreEqual(2, gameSettingList.Count());
            Assert.AreEqual(gameLobby.GameCode, lobby.GameCode);
            Assert.AreEqual(gameLobby.LobbyOwnerId, lobby.LobbyOwnerId);
            Assert.AreEqual("4" ,gameSettingList.Find(s => s.Setting.Equals("MaxPlayers")).Value);
            Assert.AreEqual("10", gameSettingList.Find(s => s.Setting.Equals("NoOfRounds")).Value);
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
            var gameLobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(lobbyId);

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