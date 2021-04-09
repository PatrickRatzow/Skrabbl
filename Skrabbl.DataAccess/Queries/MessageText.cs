using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText: ICommandText
    {
        public string GetAllMessages => @"
            SELECT * FROM ChatMessage m
            INNER JOIN Users u ON u.Id = m.UserId
            INNER JOIN Game g ON m.GameId = g.Id AND g.Id = @GameId";
        public string SaveMessage =>
            @"
            insert into ChatMessage(Message, CreatedAt, GameId, UserId) 
            Values(@Message, @CreatedAt, @GameId, @UserId)";
        public string RemoveAllMessages => "Remove from ChatMessage";
    }
}
