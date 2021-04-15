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

        public async Task<User> GetUserById(int id)
        {
            return await WithConnection(async conn =>
                await conn.QuerySingleAsync<User>(_commandText.GetUserById, new { Id = id })
            );
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await WithConnection(async conn =>
                await conn.QuerySingleAsync<User>(_commandText.GetUserByUsername, new { Username = username })
            );
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

        public async Task AddUserToLobby(int userId, string gameCode)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddUserToLobby, new { GameLobbyId = gameCode, Id = userId, });
            });
        }
    }
}