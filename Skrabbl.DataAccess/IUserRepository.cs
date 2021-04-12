using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IUserRepository
    {
        ValueTask<User> GetUserById(int id);
        ValueTask<User> GetUserByUsername(string username);
        Task<int> AddUser(User entity);
        Task DeleteUserById(int id);
        Task AddUserToLobby(int userId, string gameCode);
    }
}