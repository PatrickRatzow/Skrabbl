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
            //_gameLobbyRepository.RemoveGameLobby("1111");
        }

        [Test]
        public async Task AddGameLobbyToDb()
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
        public async Task FindGameLobbyByOwnerId()
        {
            //Arrange
            int userId = 26;

            //Act
            var gameLobby = await _gameLobbyRepository.GetLobbyByOwnerId(userId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test]
        public async Task FindGameLobbyById()
        {
            //Arrange
            string lobbyId = "1111";

            //Act
            var gameLobby = await _gameLobbyRepository.GetGameLobbyById(lobbyId);

            //Assert
            Assert.IsNotNull(gameLobby);
        }

        [Test]
        public async Task RemoveGameLobby()
        {
            //Arrange
            string lobbyId = "1223";

            //Act
            int rowsAffected = await _gameLobbyRepository.RemoveGameLobby(lobbyId);

            //Assert
            Assert.AreEqual(rowsAffected, 1);
            
        }

        [Test]
        public async Task GetAllLobbies()
        {
            //Arrange

            //Act
            var list =await _gameLobbyRepository.GetAllGameLobbies();

            //Assert

        }

        [OneTimeTearDown]
        public void TearDown()
        {
           // _gameLobbyRepository.RemoveGameLobby("1111");
        }

    }
}
