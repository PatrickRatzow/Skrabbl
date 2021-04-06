using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText: ICommandText
    {
        public string GetAllMessages => "select * from Message";
        public string SaveMessage => 
            @"
            insert into ChatMessage(message, createdAt, gameId, userId) 
            Values(@messageTemp, @createdAtTemp, @gameIdTemp, @userIdTemp)";
    }
}
