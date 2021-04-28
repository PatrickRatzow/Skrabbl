using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [TestFixture]
    [Order(2)]
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
        [Order(1)]
        public async Task GetGameFromDbTestByValidId()
        {
            //Act
            var game = await _gameRepository.GetGame(TestData.Games.PatrickGame.Id);

            //Assert
            Assert.IsNotNull(game);
        }

        [Test]
        [Order(1)]
        public async Task GetGameFromDbTestByInvalidId()
        {
            //Act
            var game = await _gameRepository.GetGame(99999999);

            //Assert
            Assert.IsNull(game);
        }

        [Test]
        [Order(1)]
        public async Task AddGameToDb()
        {
            // Act
            var game = await _gameRepository.AddGame();
            TestData.Games.FlorisGame = game;

            // Assert
            Assert.IsNotNull(game);
        }

        [Test]
        [Order(1)]
        public async Task GetCurrentTurn()
        {
            // Act
            var turn = await _gameRepository.GetCurrentTurn(TestData.Users.Patrick.Id);

            // Assert
            Assert.IsNotNull(turn);
            Assert.AreEqual(TestData.Games.PatrickGame.Rounds
                    .First()
                    .Turns.First()
                    .Id,
                turn.Id
            );
        }

        private static IEnumerable<Tuple<User, bool>> GuessedWordTestCases
        {
            get
            {
                yield return new Tuple<User, bool>(TestData.Users.Patrick, true);
                yield return new Tuple<User, bool>(TestData.Users.Simon, false);
                yield return new Tuple<User, bool>(TestData.Users.Nikolaj, false);
            }
        }

        [TestCaseSource(nameof(GuessedWordTestCases))]
        [Order(1)]
        public async Task HasUserGuessedWordForCurrentTurn(Tuple<User, bool> tuple)
        {
            // Arrange
            var (user, expectedResult) = tuple;

            // Act
            var result = await _gameRepository.HasUserGuessedWordForCurrentTurn(user.Id);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        private static IEnumerable<Tuple<Game, bool>> GoToNextRoundTestCases
        {
            get
            {
                yield return new Tuple<Game, bool>(TestData.Games.PatrickGame, true);
                yield return new Tuple<Game, bool>(TestData.Games.PatrickGame, true);
                yield return new Tuple<Game, bool>(TestData.Games.PatrickGame, true);
                yield return new Tuple<Game, bool>(TestData.Games.PatrickGame, true);
                // Fail due to having reached max round.
                yield return new Tuple<Game, bool>(TestData.Games.PatrickGame, false);
                yield return new Tuple<Game, bool>(TestData.Games.NikolajGame, true);
            }
        }

        [TestCaseSource(nameof(GoToNextRoundTestCases))]
        [Parallelizable(ParallelScope.Self)]
        [Order(2)]
        public async Task GoToNextRound(Tuple<Game, bool> tuple)
        {
            // Arrange
            var (game, expectedResult) = tuple;

            // Act
            var result = await _gameRepository.GoToNextRound(game.Id);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}