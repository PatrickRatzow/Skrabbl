using System;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;

        public UserService(IUserRepository userRepo, ICryptographyService cryptographyService)
        {
            _userRepository = userRepo;
            _cryptographyService = cryptographyService;
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

            byte[] salt = Convert.FromBase64String(user.Salt);
            bool equal = _cryptographyService.AreEqual(_password, user.Password, salt);
            if (equal)
            {
                return user;
            }

            return null;
        }

        public async Task<User> GetUser(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public Task AddToLobby(int userId, string gameCode)
        {
            return _userRepository.AddUserToLobby(userId, gameCode);
        }
    }
}