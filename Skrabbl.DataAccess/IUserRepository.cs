using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task<int> AddUser(User entity);
        Task DeleteUserById(int id);
        Task AddUserToLobby(int userId, string gameCode);
    }
}