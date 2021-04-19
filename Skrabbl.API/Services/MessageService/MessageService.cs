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

        public async Task<ChatMessage> CreateMessage(string message, int gameId, int userId)
        {
            ChatMessage msg = new ChatMessage
            {
                Message = message,
                CreatedAt = DateTime.Now,
                Game = new Game {Id = gameId},
                User = new User {Id = userId}
            };
            await _messageRepository.SaveMessage(msg);

            return msg;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessages(int lobbyId)
        {
            return await _messageRepository.GetAllMessages(lobbyId);
        }

        public Task<IEnumerable<ChatMessage>> GetMessagesByUserId(int userId)
        {
            return _messageRepository.GetAllMessagesByUserId(userId);
        }
    }
}