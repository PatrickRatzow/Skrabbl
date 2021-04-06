using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [TestFixture]
    public class UserRepositorySpec
    {
        private IUserRepository _userRepository;
        private List<int> _userIds;
        
        [SetUp]
        public void Setup()
        {
            var config = new ConfigFixture().Config;
            var cmd = new CommandText();

            _userRepository = new UserRepository(config, cmd);
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