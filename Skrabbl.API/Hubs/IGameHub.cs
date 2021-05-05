using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public interface IGameHub
    {
        Task GuessedWord(string userName);
        Task ReceiveMessage(string userName, string message);
        Task DeletedMessage(string userName, string message);
        Task ReceiveDrawNode(string color, int size, int x1, int y1, int x2, int y2);
        Task GameLobbyDisconnected(string lobbyId);
        Task SendRoundStatus();
        Task SendGameIsOver();
        Task SendSettingChanged(string key, string value);
        Task ConfirmControlTakeOver();
    }
}