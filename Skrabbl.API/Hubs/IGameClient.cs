using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public interface IGameClient
    {
        Task SendMessage(int gameId, int userId, string message);
        Task DeleteMessage(string userName, string message);
        Task GetAllMessages(int lobbyId);
        Task SendDrawNode(string color, int size, int x1, int y1, int x2, int y2);
        Task CreateLobby(string lobbyId);
        Task JoinLobby(int userId, string gameCode);
    }
}