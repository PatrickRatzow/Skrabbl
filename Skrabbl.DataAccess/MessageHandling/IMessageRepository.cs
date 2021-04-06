using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IMessageRepository
    {
        public Task<IEnumerable<ChatMessage>> GetAllMessages(int gameLobbyId);
        public Task<int> SaveMessage(ChatMessage message, int gameLobbyId);
    }
}
