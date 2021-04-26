using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql.Test
{
    [TestFixture]
    public class UserRepositorySpec
    {
        private IUserRepository _userRepository;
        private List<int> _userIds;

        [SetUp]
        public void Setup()
        {
            var cmd = new CommandText();

            _userRepository = new UserRepository(ConfigFixture.Config, cmd);
            _userIds = new List<int>();
        }

        [TestCase("patrickratzow", "patrickratzow@ucn.dk", "password1", "salt1")]
        [TestCase("nikolajjensen", "nikolajjensen@ucn.dk", "password2", "salt2")]
        public async Task AddUser(string username, string email, string password, string salt)
        {
            // Arrange
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Salt = salt
            };

            // Act
            var id = await _userRepository.AddUser(user);
            _userIds.Add(id);

            // Assert
            Assert.IsNotNull(id);
        }

        [TestCase("patrickratzow", "patrickratzow@ucn.dk", "password1", "salt1")]
        [TestCase("nikolajjensen", "nikolajjensen@ucn.dk", "password2", "salt2")]
        public async Task GetUserById(string username, string email, string password, string salt)
        {
            //Arrange
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Salt = salt
            };
            var id = await _userRepository.AddUser(user);
            _userIds.Add(id);

            //Act
            User gottenUser = await _userRepository.GetUserById(id);

            //Assert
            Assert.AreEqual(gottenUser.Username, username);
            Assert.AreEqual(gottenUser.Password, password);
            Assert.AreEqual(gottenUser.Email, email);
            Assert.AreEqual(gottenUser.Salt, salt);
        }

        [TestCase("patrickratzow", "patrickratzow@ucn.dk", "password1", "salt1")]
        [TestCase("nikolajjensen", "nikolajjensen@ucn.dk", "password2", "salt2")]
        public async Task GetUserByUsername(string username, string email, string password, string salt)
        {
            //Arrange
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Salt = salt
            };
            var id = await _userRepository.AddUser(user);
            _userIds.Add(id);

            //Act
            User gottenUser = await _userRepository.GetUserByUsername(username);

            //Assert
            Assert.AreEqual(gottenUser.Username, username);
            Assert.AreEqual(gottenUser.Password, password);
            Assert.AreEqual(gottenUser.Email, email);
            Assert.AreEqual(gottenUser.Salt, salt);
        }

        [TestCase("patrickratzow", "patrickratzow@ucn.dk", "password1", "salt1")]
        [TestCase("nikolajjensen", "nikolajjensen@ucn.dk", "password2", "salt2")]
        public async Task DeleteUserById(string username, string email, string password, string salt)
        {
            //Arrange
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Salt = salt
            };
            var id = await _userRepository.AddUser(user);
            _userIds.Add(id);


            //Act
            await _userRepository.DeleteUserById(id);
            User gottenUser = await _userRepository.GetUserById(id);

            //Assert
            Assert.IsNull(gottenUser);
        }

        [TestCase("patrickratzow", "patrickratzow@ucn.dk", "password1", "salt1", "2234")]
        [TestCase("nikolajjensen", "nikolajjensen@ucn.dk", "password2", "salt2", "1111")]
        public async Task AddUserToLobby(string username, string email, string password, string salt, string lobby)
        {
            //Arrange
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Salt = salt
            };
            var id = await _userRepository.AddUser(user);
            _userIds.Add(id);


            //Act
            await _userRepository.AddUserToLobby(id, lobby);
            User gottenUser = await _userRepository.GetUserById(id);

            //Assert
            Assert.AreEqual(gottenUser.GameLobbyId, lobby);
        }

        [TearDown]
        public void TearDown()
        {
            // Delete all user ids
            foreach (var id in _userIds)
            {
                _userRepository.DeleteUserById(id);
            }
        }
    }
}