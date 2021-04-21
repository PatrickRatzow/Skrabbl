namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetAllMessages => @"
            SELECT * FROM ChatMessage m
            INNER JOIN Users u ON u.Id = m.UserId
            INNER JOIN Game g ON m.GameId = g.Id AND g.Id = @GameId";

        public string GetAllMessagesByUserId => @"
            SELECT * FROM ChatMessage m
            INNER JOIN Game g ON m.GameId = g.Id
            INNER JOIN Users u ON u.GameLobbyId = g.GameLobbyId AND m.UserId = u.Id
            WHERE g.GameLobbyId = (
                SELECT u2.GameLobbyId
                FROM Users u2
                WHERE u2.Id = @UserId
            )";

        public string SaveMessage =>
            @"
            insert into ChatMessage(Message, CreatedAt, GameId, UserId) 
            Values(@Message, @CreatedAt, @GameId, @UserId)";

        public string RemoveAllMessages => "Remove from ChatMessage";
    }
}