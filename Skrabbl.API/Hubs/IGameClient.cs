using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public interface IGameClient
    {
        Task SendMessage(string message);
        Task DeleteMessage(string userName, string message);
        Task GetAllMessages();
        Task SendDrawNode(string color, int size, int x1, int y1, int x2, int y2);
        Task ChooseWord(int gameId, string chosenWord);
    }
}