using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private readonly ICommandText _commandText;

        public GameRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task<Game> AddGame()
        {
            return await WithConnection(async conn =>
            {
                var id = await conn.QuerySingleAsync<int>(_commandText.AddGame);

                return new Game
                {
                    Id = id
                };
            });
        }

        public async Task<Game> GetGame(int id)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Game>(_commandText.GetGameById, new {Id = id});
            });
        }

        public async Task<Turn> GetCurrentTurn(int userId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Turn>(_commandText.GetCurrentTurnByUserId, new
                {
                    UserId = userId
                });
            });
        }

        public async Task<bool> HasUserGuessedWordForCurrentTurn(int userId)
        {
            return await WithConnection(async conn =>
            {
                var turn = await conn.QueryFirstOrDefaultAsync<Turn>(
                    _commandText.GetCurrentTurnByUserIfIdUserHasGuessedWord, new
                    {
                        UserId = userId
                    });

                return turn != null;
            });
        }

        private class GameRoundStatus
        {
            public int NumberOfRounds { get; }
            public int ActiveRoundNumber { get; }
        }

        public Task<bool> GoToNextRound(int gameId)
        {
            return WithConnection(async conn =>
            {
                var gameRoundStatus = await conn.QuerySingleOrDefaultAsync<GameRoundStatus>(
                    _commandText.GetRoundStatusForGame, new
                    {
                        GameId = gameId
                    });

                // If the next round would be higher than total amount of rounds we exit
                if (gameRoundStatus.ActiveRoundNumber + 1 > gameRoundStatus.NumberOfRounds)
                    return false;

                var affectedRows = await conn.ExecuteAsync(_commandText.SetRoundNumberForGame, new
                {
                    GameId = gameId,
                    RoundNumber = gameRoundStatus.ActiveRoundNumber
                });

                return affectedRows > 0;
            });
        }
    }
}