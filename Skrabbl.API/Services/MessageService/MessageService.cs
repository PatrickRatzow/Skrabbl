using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class MessageService : IMessageService
    {
        IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepo)
        {
            _messageRepository = messageRepo;
        }

        public async Task<ChatMessage> CreateMessage(string message, int userId)
        {
            ChatMessage msg = new ChatMessage
            {
                Message = message,
                CreatedAt = DateTime.Now,
                User = new User {Id = userId}
            };
            await _messageRepository.SaveMessage(msg);

            return msg;
        }

        public Task<IEnumerable<ChatMessage>> GetMessagesByUserId(int userId)
        {
            return _messageRepository.GetAllMessagesByUserId(userId);
        }
    }
}