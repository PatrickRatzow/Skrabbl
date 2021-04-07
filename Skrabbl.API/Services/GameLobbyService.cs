﻿
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using Skrabbl.Model.Errors;
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
            GameLobby lobby = new GameLobby { 
                GameCode = gameCode,
                LobbyOwnerId = userId
            };

            await _gameLobbyRepository.AddGameLobby(lobby);

            return lobby;
        }

        public async Task<bool> RemoveGameLobby(string id)
        {
            var exisitngLobby = await GetGameLobbyById(id);
            if (exisitngLobby != null)
            {
                await _gameLobbyRepository.RemoveGameLobby(id);
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<GameLobby> GetGameLobbyById(string lobbyId)
        {
            return await _gameLobbyRepository.GetGameLobbyById(lobbyId);
        }

        public async Task<IEnumerable<GameLobby>> GetAllGameLobbies()
        {
            return await _gameLobbyRepository.GetAllLobbies();
        }

        public async Task<GameLobby> GetLobbyByOwnerId(int ownerId)
        {
            return await _gameLobbyRepository.GetLobbyByOwnerId(ownerId);
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
