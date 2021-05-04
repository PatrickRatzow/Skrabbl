using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.Model;

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
        Task UpdateGameLobbySetting(GameSetting gameSetting, int lobbyOwnerId);
    }
}