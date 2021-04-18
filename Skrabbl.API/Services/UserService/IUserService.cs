using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(string _userName, string _password, string _email);
        Task<User> GetUser(int id);
        Task<User> GetUser(string _username, string _password);
        Task<User> GetUserByRefreshToken(string token);
        Task AddToLobby(int userId, string gameCode);
    }
}