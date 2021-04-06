using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IGameLobbyService
    {
        Task AddGameLobby(GameLobby lobby);
        Task RemoveGameLobby(string id);
        Task<GameLobby> GetGameLobbyById(string id);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
        Task<bool> UpdateGameLobby(GameLobby lobby);
    }
}
