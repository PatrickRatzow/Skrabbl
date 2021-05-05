using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System.Linq;
using System.Threading;
using Skrabbl.Model.Errors;

namespace Skrabbl.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IGameLobbyRepository _gameLobbyRepository;

        private readonly static SemaphoreSlim _addingUserToLobbySemaphore = new SemaphoreSlim(1, 1);

        public UserService(IUserRepository userRepo, ICryptographyService cryptographyService, IGameLobbyRepository gameLobbyRepository)
        {
            _userRepository = userRepo;
            _cryptographyService = cryptographyService;
            _gameLobbyRepository = gameLobbyRepository;
        }


        public async Task<User> CreateUser(string _userName, string _password, string _email)
        {
            User current = new User();
            current.Username = _userName;

            byte[] salt = _cryptographyService.CreateSalt();
            current.Password = _cryptographyService.GenerateHash(_password, salt);
            current.Email = _email;
            current.Salt = Convert.ToBase64String(salt);

            current.Id = await _userRepository.AddUser(current);

            return current;
        }

        public async Task<User> GetUser(string _username, string _password)
        {
            User user = await _userRepository.GetUserByUsername(_username);
            if (user == null) return null;

            byte[] salt = Convert.FromBase64String(user.Salt);
            bool equal = _cryptographyService.AreEqual(_password, user.Password, salt);
            if (equal)
            {
                return user;
            }

            return null;
        }

        public async Task<User> GetUserByRefreshToken(string token)
        {
            return await _userRepository.GetUserByRefreshToken(token);
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task AddToLobby(int userId, string gameCode)
        {
            try
            {
                await _addingUserToLobbySemaphore.WaitAsync();
                var users = await _userRepository.GetUsersByGameCode(gameCode);
                int numberOfUsers = users.ToList().Count();
                var gameSettings = await _gameLobbyRepository.GetGameSettingsByGameCode(gameCode);
                int maxPlayers = Convert.ToInt32(gameSettings.ToList().Find(s => s.Setting.Equals("MaxPlayers")).Value);

                if (numberOfUsers >= maxPlayers)
                {
                    throw new LobbyIsFullException("Too many players");
                }
                await _userRepository.AddUserToLobby(userId, gameCode);
            }
            finally
            {
                _addingUserToLobbySemaphore.Release();
            }
        }
    }
}