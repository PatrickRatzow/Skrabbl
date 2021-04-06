using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IUserRepository
    {
        ValueTask<User> GetUserById(int id);
        Task<int> AddUser(User entity);
        Task DeleteUserById(int id);
    }
}