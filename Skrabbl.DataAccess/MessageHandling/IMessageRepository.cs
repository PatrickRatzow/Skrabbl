using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<ChatMessage>> GetAllMessages(int gameLobbyId);
        public Task<IEnumerable<ChatMessage>> GetAllMessagesByUserId(int userId);
        public Task SaveMessage(ChatMessage message);
        public Task RemoveAllChatMessages();
    }
}