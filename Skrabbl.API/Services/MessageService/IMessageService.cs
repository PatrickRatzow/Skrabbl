using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<ChatMessage>> GetMessages(int lobbyId);
        Task<IEnumerable<ChatMessage>> GetMessagesByUserId(int userId);
        Task<ChatMessage> CreateMessage(string message, int userId);
    }
}