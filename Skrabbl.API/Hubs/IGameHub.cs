using Skrabbl.Model.Dto;
using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public interface IGameHub
    {
        Task GuessedWord(string userName);
        Task ReceiveMessage(string userName, string message);
        Task DeletedMessage(string userName, string message);
        Task ReceiveDrawNode(CommandDto commandDto);
        Task GameLobbyDisconnected(string lobbyId);
    }
}