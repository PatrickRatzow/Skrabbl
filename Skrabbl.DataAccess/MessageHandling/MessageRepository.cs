using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        private readonly ICommandText _commandText;

        public MessageRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessages(int gameLobbyId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<ChatMessage, User, ChatMessage>(_commandText.GetAllMessages,
                    (chatMessage, user) =>
                    {
                        chatMessage.User = user;

                        return chatMessage;
                    }, new
                    {
                        GameId = gameLobbyId
                    },
                    splitOn: "UserId"
                );
            });
        }

        public Task<IEnumerable<ChatMessage>> GetAllMessagesByUserId(int userId)
        {
            return WithConnection(async conn =>
            {
                return await conn.QueryAsync<ChatMessage, User, ChatMessage>(_commandText.GetAllMessagesByUserId,
                    (chatMessage, user) =>
                    {
                        chatMessage.User = user;

                        return chatMessage;
                    }, new
                    {
                        UserId = userId
                    },
                    splitOn: "UserId"
                );
            });
        }

        public async Task SaveMessage(ChatMessage message)
        {
            await WithConnection(async conn =>
            {
                return await conn.ExecuteAsync(_commandText.SaveMessage, new
                {
                    Message = message.Message,
                    CreatedAt = message.CreatedAt,
                    GameId = message.Game.Id,
                    UserId = message.User.Id
                });
            });
        }

        public async Task RemoveAllChatMessages()
        {
            await WithConnection(async conn => { await conn.ExecuteAsync(_commandText.RemoveAllMessages); });
        }
    }
}