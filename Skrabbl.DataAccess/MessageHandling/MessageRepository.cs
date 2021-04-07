using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Skrabbl.Model;
using System.Threading.Tasks;

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
                return await conn.QueryAsync<ChatMessage>(_commandText.GetAllMessages);
            });

        }

        public async Task SaveMessage(ChatMessage message)
        {
            await WithConnection(async conn =>
            {
                return await conn.ExecuteAsync(_commandText.SaveMessage, new {
                    Message = message.Message, 
                    CreatedAt = message.CreatedAt,
                    GameId = message.Game.Id,
                    UserId = message.User.Id
                });

            });
        }
        public async Task RemoveAllChatMessages()
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.RemoveAllMessages);
            });
        }

    }
}

