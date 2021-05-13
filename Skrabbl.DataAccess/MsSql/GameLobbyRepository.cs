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
                    new { Code = lobbyCode });
            });
        }

        public async Task AddGameLobby(GameLobby entity)
        {
            await WithConnection(async conn =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Code", entity.Code);
                parameters.Add("@LobbyOwnerId", entity.LobbyOwnerId);

                var i = 0;
                var inserts = string.Join(", ", entity.GameSettings.Select(x =>
                {
                    var gameCodeId = ++i;
                    parameters.Add($@"P{gameCodeId}", x.GameLobbyCode);
                    var settingId = ++i;
                    parameters.Add($@"P{settingId}", x.SettingType);
                    var valueId = ++i;
                    parameters.Add($@"P{valueId}", x.Value);

                    return $"(@P{gameCodeId}, @P{settingId}, @P{valueId})";
                }));
                var query = $@"{_commandText.AddLobby}";

                if (!string.IsNullOrEmpty(inserts))
                {
                    query += $@";
                        INSERT INTO GameSetting(GameLobbyCode, SettingType, Value)
                        VALUES {inserts}";
                }

                await conn.ExecuteAsync(query, parameters);
            });
        }

        public async Task<int> RemoveGameLobby(string ownerId)
        {
            return await WithConnection(async conn =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Code", ownerId);

                var query = "Delete from GameSetting where GameLobbyCode = @Code;";

                query += $@"{_commandText.RemoveLobbyById}";

                return await conn.ExecuteAsync(query, parameters);
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

        public async Task<IEnumerable<GameSetting>> GetGameSettingsByGameCode(string gameCode)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<GameSetting>(_commandText.GetGameSettingsByGameCode,
                    new { GameLobbyCode = gameCode });
            });
        }

        public async Task UpdateGameLobbySetting(GameSetting gameSetting, int lobbyOwnerId)
        {
            await WithConnection(conn =>
            {
                return conn.ExecuteAsync(_commandText.UpdateGameSetting, new
                {
                    Value = gameSetting.Value,
                    Setting = gameSetting.SettingType,
                    LobbyOwnerId = lobbyOwnerId
                });
            });
        }
    }
}