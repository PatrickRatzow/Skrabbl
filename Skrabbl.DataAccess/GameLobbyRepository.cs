using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System.Collections.Generic;
using System.Linq;
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

        public async ValueTask<GameLobby> GetGameLobbyById(string ownerId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<GameLobby>(_commandText.GetLobbyById, 
                    new { GameCode = ownerId});
            });
        }

        public async Task AddGameLobby(GameLobby entity)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddLobby, entity);
            });
        }

        public async Task RemoveGameLobby(string ownerId)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveLobbyById, new { GameCode = ownerId });
            });
        }

        public async Task RemoveAllGameLobbies()
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveAllLobbies);
            });
        }
        public async ValueTask<IEnumerable<GameLobby>> GetAllLobbies()
        {
            return await WithConnection<IEnumerable<GameLobby>>(async conn =>
            {
                return await conn.QueryAsync<GameLobby>(_commandText.GetAllLobbies);
            });
        }

        public async ValueTask<GameLobby> GetLobbyByOwnerId(int ownerId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<GameLobby>(_commandText.GetLobbyByOwnerId, 
                    new { LobbyOwnerId = ownerId });
            });
        }
    }
}
