namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        // TODO: Outdated query, game no longer has a GameId column
        public string GetAllMessages => @"
            SELECT * FROM ChatMessage m
            INNER JOIN [User] u ON u.Id = m.UserId
            INNER JOIN Game g ON m.GameId = g.Id AND g.Id = @GameId";

        public string GetAllMessagesByUserId => @"
            SELECT
                m.*,
                u.*
            FROM ChatMessage m
            INNER JOIN Turn t ON m.TurnId = t.Id
            INNER JOIN Round r ON t.Id = r.ActiveTurnId
            INNER JOIN Game g ON r.GameId = g.Id
            INNER JOIN GameLobby gl ON g.Id = gl.GameId
            INNER JOIN [User] u ON u.LobbyCode = gl.Code AND m.UserId = u.Id
            WHERE gl.Code = (
                SELECT u2.LobbyCode
                FROM [User] u2
                WHERE u2.Id = @UserId
            )";

        public string SaveMessage => @"
            INSERT INTO ChatMessage(Message, CreatedAt, UserId, TurnId)
            VALUES(
                @Message, 
                @CreatedAt, 
                @UserId, 
                (
                    SELECT
                        t.Id
                    FROM Turn t
                    INNER JOIN Round r ON r.ActiveTurnId = t.Id
                    INNER JOIN Game g ON g.ActiveRoundId = r.Id
                    INNER JOIN GameLobby gl ON gl.GameId = g.Id
                    INNER JOIN [User] u ON u.Id = @UserId
                    WHERE u.LobbyCode = gl.Code
                )
            )";

        public string RemoveAllMessages => "Remove from ChatMessage";
    }
}