
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

        public async Task<GameLobby> AddGameLobby(int userId)
        {
            string gameCode = null;

            while (true)
            {
                try
                {
                    gameCode = GenerateGameLobbyCode();
                    await GetGameLobbyById(gameCode);
                } catch
                {
                    break;
                }
            }
            GameLobby gameLobby = new GameLobby { 
                GameCode = gameCode,
                LobbyOwnerId = userId
            };

            await _gameLobbyRepository.AddGameLobby(gameLobby);

            return gameLobby;
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

        private string GenerateGameLobbyCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
