using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using Skrabbl.Model.Errors;

namespace Skrabbl.API.Services
{
    public class GameLobbyService : IGameLobbyService
    {
        private IGameLobbyRepository _gameLobbyRepository;

        public GameLobbyService(IGameLobbyRepository gameLobbyRepo)
        {
            _gameLobbyRepository = gameLobbyRepo;
        }

        public async Task<GameLobby> AddGameLobby(int userId, List<GameSettingDto>? gameSettings = null)
        {
            var existingLobby = await GetLobbyByOwnerId(userId);

            if (existingLobby != null)
            {
                throw new UserAlreadyHaveALobbyException();
            }

            string gameCode = null;

            while (true)
            {
                gameCode = GenerateGameLobbyCode();
                var gameLobby = await GetGameLobbyById(gameCode);

                if (gameLobby == null)
                {
                    break;
                }
            }

            GameLobby lobby = new GameLobby
            {
                GameCode = gameCode,
                LobbyOwnerId = userId,
                GameSettings = Map(gameSettings ?? new List<GameSettingDto>(), gameCode)
            };

            await _gameLobbyRepository.AddGameLobby(lobby);

            return lobby;
        }

        public async Task<bool> RemoveGameLobby(string id)
        {
            var existingLobby = await GetGameLobbyById(id);
            if (existingLobby != null)
            {
                await _gameLobbyRepository.RemoveGameLobby(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<GameLobby> GetGameLobbyById(string lobbyCode)
        {
            return await _gameLobbyRepository.GetGameLobbyByLobbyCode(lobbyCode);
        }

        public async Task<IEnumerable<GameLobby>> GetAllGameLobbies()
        {
            return await _gameLobbyRepository.GetAllGameLobbies();
        }

        public async Task<GameLobby> GetLobbyByOwnerId(int ownerId)
        {
            return await _gameLobbyRepository.GetLobbyByOwnerId(ownerId);
        }

        public async Task<List<GameSetting>> GetGameSettingsByGameId(int gameId)
        {
            IEnumerable<GameSetting> gameSettingList = await _gameLobbyRepository.GetGameSettingsByGameCode(gameId);

            return gameSettingList.ToList();
        }
        public async Task SetGameSettingsByGameCode(GameSetting gameSetting)
        {
            await _gameLobbyRepository.SetGameSettingsByGameCode(gameSetting);
        }

        public async Task<GameLobby> UpdateGameSetting(int userId, List<GameSettingDto> gameSettingList)
        {
            var existing = await GetLobbyByOwnerId(userId);

            await _gameLobbyRepository.UpdateGameLobbySettings(Map(gameSettingList, existing.GameCode), existing);
            return await GetLobbyByOwnerId(userId);

        }
        private List<GameSetting> Map(List<GameSettingDto> gameSettings, string gameCode)
        {
            return gameSettings.ConvertAll(g => new GameSetting
            {
                Value = g.Value,
                Setting = g.Setting,
                GameCode = gameCode
            });

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