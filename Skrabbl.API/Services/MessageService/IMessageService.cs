using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<ChatMessage>> GetMessages(int lobbyId);
        Task<ChatMessage> CreateMessage(string message, int gameId, int userId);
    }
}
