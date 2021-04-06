using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ICommandText _commandText;
        
        public UserRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }
        
        public ValueTask<User> GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUser(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}