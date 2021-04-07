using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public class MessageService : IMessageService
    {

        IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepo)
        {
            _messageRepository = messageRepo;
        }

        public async Task<ChatMessage> CreateMessage(string message, int gameId, int userId)
        {
            ChatMessage msg = new ChatMessage();
            msg.Message = message;
            msg.CreatedAt = DateTime.Now;
            msg.Game = new Game { Id = gameId };
            msg.User = new User { Id = userId };

            await _messageRepository.SaveMessage(msg);

            return msg;
        }

        public Task<IEnumerable<ChatMessage>> GetMessages(int lobbyId)
        {
            throw new NotImplementedException();
        }
    }
}
