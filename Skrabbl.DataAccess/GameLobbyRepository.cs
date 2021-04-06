using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public class GameLobbyRepository : BaseRepository, IGameLobbyRepository
    {
        private readonly ICommandText _commandText;

        public GameLobbyRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async ValueTask<GameLobby> GetGameLobbyById(string id)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleAsync<GameLobby>(_commandText.GetLobbyById, new { GameCode = id});
            });
        }

        public async Task AddGameLobby(GameLobby entity)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddLobby, entity);
            });
        }

        public async Task RemoveGameLobby(string id)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveLobbyById, new { Id = id });
            });
        }

        public async Task RemoveAllGameLobbies()
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveAllLobbies);
            });
        }
        public ValueTask<IEnumerable<GameLobby>> GetAllLobbies()
        {
            throw new System.NotImplementedException();
        }
    }
}
