using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public class GameLobbyRepository : BaseRepository, IGameLobbyRepository
    {
        private readonly ICommandText _commandText;

        public GameLobbyRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task<GameLobby> GetGameLobbyByLobbyCode(string lobbyCode)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<GameLobby>(_commandText.GetLobbyByLobbyCode,
                    new { GameCode = lobbyCode });
            });
        }

        public async Task AddGameLobby(GameLobby entity)
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(_commandText.AddLobby, entity); });
        }

        public async Task<int> RemoveGameLobby(string ownerId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.ExecuteAsync(_commandText.RemoveLobbyById, new { GameCode = ownerId });
            });
        }

        public async Task RemoveAllGameLobbies()
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(_commandText.RemoveAllLobbies); });
        }

        public async Task<IEnumerable<GameLobby>> GetAllGameLobbies()
        {
            return await WithConnection<IEnumerable<GameLobby>>(async conn =>
            {
                return await conn.QueryAsync<GameLobby>(_commandText.GetAllLobbies);
            });
        }

        public async Task<GameLobby> GetLobbyByOwnerId(int ownerId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QuerySingleOrDefaultAsync<GameLobby>(_commandText.GetLobbyByOwnerId,
                    new { LobbyOwnerId = ownerId });
            });
        }
        public async Task<IEnumerable<GameSetting>> GetGameSettingsByGameId(int gameId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.GetGameSettingsByGameId,
                    new { GameId = gameId });
            });
        }

        public async Task SetGameSettingsByGameId(int gameId, string setting)
        {
            await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.SetGameSettingsByGameId,
                    new
                    {
                        GameId = gameId,
                        Setting = setting
                    });
            });
        }
    }
}