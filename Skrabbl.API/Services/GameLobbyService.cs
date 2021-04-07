
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public class GameLobbyService : IGameLobbyService
    {

        private IGameLobbyRepository _gameLobbyRepository;

        public GameLobbyService(IGameLobbyRepository gameLobbyRepo)
        {
            _gameLobbyRepository = gameLobbyRepo;
        }

        public async Task AddGameLobby(GameLobby toAdd)
        {
            await _gameLobbyRepository.AddGameLobby(toAdd);
        }

        public async Task RemoveGameLobby(string id)
        {
           await _gameLobbyRepository.RemoveGameLobby(id);
        }

        public async Task<GameLobby> GetGameLobbyById(string id)
        {

            return await _gameLobbyRepository.GetGameLobbyById(id);
        }

        public async Task<IEnumerable<GameLobby>> GetAllGameLobbies()
        {
            return await _gameLobbyRepository.GetAllLobbies();
        }

        public Task<bool> UpdateGameLobby(GameLobby toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
