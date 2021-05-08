using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

namespace Skrabbl.API.Services
{
    public interface IGameLobbyService
    {
        Task<GameLobby> AddGameLobby(int userId, List<GameSettingDto>? gameSettings = null);
        Task<bool> RemoveGameLobby(string id);
        Task<GameLobby> GetGameLobbyById(string lobbyId);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
        Task<GameLobby> GetLobbyByOwnerId(int ownerId);
        Task<List<GameSetting>> GetGameSettingsByGameId(string gameCode);
        Task UpdateGameSetting(GameSetting gameSetting, int lobbyOwnerId);
    }
}