using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IGameLobbyRepository
    {
        Task<GameLobby> GetGameLobbyByLobbyCode(string lobbyId);
        Task AddGameLobby(GameLobby entity);
        Task<int> RemoveGameLobby(string lobbyId);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
        Task RemoveAllGameLobbies();
        Task<GameLobby> GetLobbyByOwnerId(int ownerId);
        Task<IEnumerable<GameSetting>> GetGameSettingsByGameCode(int gameId);
        Task SetGameSettingsByGameCode(GameSetting gameSetting);
        Task UpdateGameLobbySettings(List<GameSetting> gameSettings, GameLobby entity);
    }
}
