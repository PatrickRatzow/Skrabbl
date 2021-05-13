using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;
using System.Linq;
using System.Diagnostics;

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
                GameLobbyCode = "a1b1",
                SettingType = "MaxPlayers",
                Value = "4"
            };
            GameSetting gameSetting1 = new GameSetting()
            {
                GameLobbyCode = "a1b1",
                SettingType = "NoOfRounds",
                Value = "10"
            };

            List<GameSetting> gameSettings = new List<GameSetting>();
            gameSettings.Add(gameSetting);
            gameSettings.Add(gameSetting1);

            TestData.GameLobbies.FlorisLobby = new GameLobby()
            {
                Code = "a1b1",
                LobbyOwnerId = TestData.Users.Floris.Id,
                GameSettings = gameSettings
            };
        }

        [Test, Order(1)]
        public async Task AddGameLobbyToDb_Success()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            await _gameLobbyRepository.AddGameLobby(gameLobby);
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(gameLobby.Code);

            //Assert
            Assert.IsNotNull(lobby);
        }

        [Test, Order(2)]
        public async Task ValideLobbyCode()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(gameLobby.Code);

            //Assert
            Assert.AreEqual(gameLobby.Code, lobby.Code);
        }

        [Test, Order(3)]
        public async Task GameLobbyLobbyOwnerIdIsEqualToLobbyOwnerId()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(gameLobby.Code);

            //Assert
            Assert.AreEqual(gameLobby.LobbyOwnerId, lobby.LobbyOwnerId);
        }

        [Test, Order(4)]
        public async Task AllGameSettingsAdded()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(gameLobby.Code);
            IEnumerable<GameSetting> gameSetting = await _gameLobbyRepository.GetGameSettingsByGameCode(gameLobby.Code);
            List<GameSetting> gameSettingList = gameSetting.ToList();

            //Assert
            Assert.AreEqual(2, gameSettingList.Count());
        }
        
        [Test, Order(5)]
        public async Task CorrectGameSettingsAdded()
        {
            //Arrange
            var gameLobby = TestData.GameLobbies.FlorisLobby!;

            //Act
            IEnumerable<GameSetting> gameSetting = await _gameLobbyRepository.GetGameSettingsByGameCode(gameLobby.Code);
            List<GameSetting> gameSettingList = gameSetting.ToList();

            //Assert
            Assert.AreEqual("4", gameSettingList.Find(s => s.SettingType.Equals("MaxPlayers")).Value);
            Assert.AreEqual("10", gameSettingList.Find(s => s.SettingType.Equals("NoOfRounds")).Value);
        }

        [Test, Order(6)]
        public async Task FindGameLobbyByOwnerId()
        {
            //Arrange
            int userId = TestData.Users.Floris.Id;

            //Act
            var gameLobby = await _gameLobbyRepository.GetLobbyByOwnerId(userId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test, Order(7)]
        public async Task FindGameLobbyById()
        {
            //Arrange
            string lobbyId = TestData.GameLobbies.FlorisLobby!.Code;

            //Act
            var gameLobby = await _gameLobbyRepository.GetGameLobbyByLobbyCode(lobbyId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test, Order(8)]
        public async Task RemoveGameLobby()
        {
            //Arrange
            string lobbyId = TestData.GameLobbies.FlorisLobby!.Code;

            //Act
            int rowsAffected = await _gameLobbyRepository.RemoveGameLobby(lobbyId);
            
            TestData.GameLobbies.FlorisLobby = null;

            //Assert
            Assert.AreEqual(rowsAffected, 3);
        }

        [Test, Order(9)]
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