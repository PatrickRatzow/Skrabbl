using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IUserRepository
    {
        ValueTask<User> GetUserById(int id);
        Task AddUser(User entity);
    }
}