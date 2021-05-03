using Skrabbl.Model;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IGameLobbyService
    {
        Task<GameLobby> AddGameLobby(int userId, List<GameSettingDto>? gameSettings = null);
        Task<bool> RemoveGameLobby(string id);
        Task<GameLobby> GetGameLobbyById(string lobbyId);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
        Task<GameLobby> GetLobbyByOwnerId(int ownerId);
        Task<List<GameSetting>> GetGameSettingsByGameId(int gameId);
        Task SetGameSettingsByGameCode(GameSetting gameSetting);
        Task<GameLobby> UpdateGameSetting(int userId, List<GameSettingDto> gameSettingList);

    }
}
