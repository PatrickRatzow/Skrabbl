using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IGameLobbyRepository
    {
        Task<GameLobby> GetGameLobbyById(string lobbyId);
        Task AddGameLobby(GameLobby entity);
        Task<int> RemoveGameLobby(string lobbyId);
        Task<IEnumerable<GameLobby>> GetAllGameLobbies();
        Task RemoveAllGameLobbies();
        Task<GameLobby> GetLobbyByOwnerId(int ownerId);
    }
}
