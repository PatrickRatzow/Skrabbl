using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test
{
    class GameLobbyRepositorySpec
    {
        IGameLobbyRepository _gameLobbyRepository;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigFixture().Config;
            var cmd = new CommandText();

            _gameLobbyRepository = new GameLobbyRepository(config, cmd);
            _gameLobbyRepository.RemoveAllGameLobbies();
        }

        [Test]
        public async Task AddGameLobbyToDbTest()
        {
            //Arrange
            GameLobby gameLobby = new GameLobby()
            {
                GameCode = "1111",
                LobbyOwnerId = 25,
            };

            //Act
            await _gameLobbyRepository.AddGameLobby(gameLobby);
            GameLobby lobby = await _gameLobbyRepository.GetGameLobbyById(gameLobby.GameCode);

            //Assert
            Assert.IsNotNull(lobby);
        }

        [Test]
        public async Task FindGameLobbyByOwnerIdTest()
        {
            //Arrange
            int userId = 26;

            //Act
            var gameLobby = await _gameLobbyRepository.GetLobbyByOwnerId(userId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _gameLobbyRepository.RemoveAllGameLobbies();
        }

    }
}
