using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;

namespace Skrabbl.DataAccess.Test
{
    class GameRepositorySpec
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
            //Act
            var game = await _gameRepository.GetGame(TestData.Games.PatrickGame.Id);

            //Assert
            Assert.IsNotNull(game);
        }

        [Test]
        public async Task GetGameFromDbTestByInvalidId()
        {
            //Act
            var game = await _gameRepository.GetGame(99999999);

            //Assert
            Assert.IsNull(game);
        }

        [Test]
        public async Task AddGameToDb()
        {
            // Act
            var game = await _gameRepository.AddGame();
            TestData.Games.FlorisGame = game;

            // Assert
            Assert.IsNotNull(game);
        }

        [Test]
        public async Task GetCurrentTurn()
        {
            // Act
            var turn = await _gameRepository.GetCurrentTurn(TestData.Users.Patrick.Id);

            // Assert
            Assert.IsNotNull(turn);
        }

        [Test]
        public async Task HasUserGuessedWordForCurrentTurn()
        {
            Assert.Ignore();
        }

        [Test]
        public async Task GoToNextRound()
        {
            Assert.Ignore();
        }
    }
}