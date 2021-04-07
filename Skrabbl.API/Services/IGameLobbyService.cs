using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IGameLobbyService
    {
        Task<GameLobby> AddGameLobby(int userId);
        Task RemoveGameLobby(string id);
        Task<GameLobby> GetGameLobbyById(string id);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
    }
}
