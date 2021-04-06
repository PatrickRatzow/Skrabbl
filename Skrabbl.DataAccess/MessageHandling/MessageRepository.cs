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
        public async Task<IEnumerable<Message>> GetAllMessages(int gameLobbyId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<Message>(_commandText.GetAllMessages);
            });
            
        }

        public async Task<SaveMessage>(Message message, int gameLobbyId)
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<Message>(_commandText.GetAllMessages);
            });
        }
            return Convert.ToBoolean(insertedResult);
        }
    }
}
