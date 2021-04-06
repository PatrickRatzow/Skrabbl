using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IMessageRepository
    {
        public async Task<IEnumerable<Message>> GetAllMessages(int gameLobbyId);
        public bool SaveMessage(Message message, int gameLobbyId);
    }
}
