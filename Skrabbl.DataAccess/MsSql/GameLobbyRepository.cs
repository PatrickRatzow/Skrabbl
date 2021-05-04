using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql
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
                    new {GameCode = lobbyCode});
            });
        }

        public async Task AddGameLobby(GameLobby entity)
        {
            await WithConnection(async conn =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GameCode", entity.GameCode);
                parameters.Add("@LobbyOwnerId", entity.LobbyOwnerId);

                var i = 0;
                var inserts = string.Join(", ", entity.GameSettings.Select(x =>
                {
                    var gameCodeId = ++i;
                    parameters.Add($@"P{gameCodeId}", x.GameCode);
                    var settingId = ++i;
                    parameters.Add($@"P{settingId}", x.Setting);
                    var valueId = ++i;
                    parameters.Add($@"P{valueId}", x.Value);

                    return $"(@P{gameCodeId}, @P{settingId}, @P{valueId})";
                }));
                var query = $@"{_commandText.AddLobby}";

                if (!string.IsNullOrEmpty(inserts))
                {
                    query += @";
                        INSERT INTO GameSetting(GameCode, Setting, Value)
                        VALUES {inserts}";
                }

                await conn.ExecuteAsync(query, parameters);
            });
        }

        public async Task<int> RemoveGameLobby(string ownerId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.ExecuteAsync(_commandText.RemoveLobbyById, new {GameCode = ownerId});
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
                    new {LobbyOwnerId = ownerId});
            });
        }

        public async Task<IEnumerable<GameSetting>> GetGameSettingsByGameCode(int gameId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.GetGameSettingsByGameCode,
                    new {GameId = gameId});
            });
        }

        public async Task SetGameSettingsByGameCode(GameSetting gameSetting)
        {
            await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.SetGameSettingsByGameCode, gameSetting);
            });
        }

        public async Task UpdateGameLobbySettings(List<GameSetting> gameSettings, GameLobby entity)
        {
            //TODO: make UpdateGameLobby to one whole transaction 
            foreach (var setting in gameSettings)
            {
                await UpdateGameSettingsByGameCode(setting);
            }
        }

        public async Task UpdateGameSettingsByGameCode(GameSetting gameSetting)
        {
            await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.UpdateGameSettingsByGameCode, gameSetting);
            });
        }
    }
}