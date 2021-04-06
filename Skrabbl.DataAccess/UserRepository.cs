using System.Threading.Tasks;
using Dapper;
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

        public async Task<int> AddUser(User entity)
        {
            return await WithConnection(async conn =>
            {
                var id = await conn.QuerySingleAsync<int>(_commandText.AddUser, entity);

                return id;
            });
        }

        public async Task DeleteUserById(int id)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveUserById, new {Id = id});
            });
        }
    }
}