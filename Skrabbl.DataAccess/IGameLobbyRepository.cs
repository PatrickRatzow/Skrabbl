using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IGameLobbyRepository
    {
        ValueTask<GameLobby> GetGameLobbyById(string lobbyId);
        Task AddGameLobby(GameLobby entity);
        Task RemoveGameLobby(string lobbyId);
        ValueTask<IEnumerable<GameLobby>> GetAllLobbies();
        Task RemoveAllGameLobbies();
        ValueTask<GameLobby> GetLobbyByOwnerId(int ownerId);
    }
}
