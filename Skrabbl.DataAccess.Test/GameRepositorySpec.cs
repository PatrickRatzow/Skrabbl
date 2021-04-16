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
    class GameRepositorySpec
    {
        class GameLobbyRepositorySpec
        {
            IGameRepository _gameRepository;

            [SetUp]
            public void Setup()
            {
                var config = new ConfigFixture().Config;
                var cmd = new CommandText();

                _gameRepository = new GameRepository(config, cmd);
            }

            [Test]
            public async Task GetGameFromDbTestById()
            {
                //Arrange
                Game game;

                //Act
                game = await _gameRepository.GetGame(4);

                //Assert
                Assert.IsNotNull(game);
            }

            [Test]
            public async Task AddGameToDb()
            {
                //Needs to be implemented
            }

            [OneTimeTearDown]
            public void TearDown()
            {
            }
        }
    }
}
