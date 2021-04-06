using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IGameLobbyRepository
    {
        ValueTask<GameLobby> GetGameLobbyById(int id);
        Task AddGameLobby(GameLobby entity);
    }
}
