using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql.Test
{
    class GameRepositorySpec
    {
        class GameLobbyRepositorySpec
        {
            IGameRepository _gameRepository;

            [SetUp]
            public void Setup()
            {
                var cmd = new CommandText();

                _gameRepository = new GameRepository(ConfigFixture.Config, cmd);
            }

            [Test]
            public async Task GetGameFromDbTestByValidId()
            {
                //Arrange
                Game game;

                //Act
                game = await _gameRepository.GetGame(4);

                //Assert
                Assert.IsNotNull(game);
            }

            [Test]
            public async Task GetGameFromDbTestByInvalidId()
            {
                //Arrange
                Game game = new Game();

                //Act
                game = await _gameRepository.GetGame(9999);

                //Assert
                Assert.IsNull(game);
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