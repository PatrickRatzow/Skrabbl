using Skrabbl.Model.Dto;
using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public interface IGameClient
    {
        Task SendMessage(string message);
        Task DeleteMessage(string userName, string message);
        Task GetAllMessages();
        Task SendDrawNode(CommandDto commandDto);
        Task CreateLobby(string lobbyId);
        Task JoinLobby(int userId, string gameCode);
        Task ChooseWord(int gameId, string chosenWord);
    }
}