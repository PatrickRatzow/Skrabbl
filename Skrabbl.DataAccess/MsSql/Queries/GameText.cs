namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetGameById => "SELECT * FROM game WHERE Id = @Id";

        public string GetCurrentTurnByUserId => @"
            SELECT
                t.*
            FROM Turn t
            INNER JOIN Round r ON t.Id = r.ActiveTurnId
            INNER JOIN Game g ON r.GameId = g.Id
            INNER JOIN Users u ON t.UserId = u.Id
            INNER JOIN GameLobby gl ON u.GameLobbyId = gl.GameCode 
               AND g.Id = gl.GameId
            WHERE u.Id = @UserId";

        public string GetCurrentTurnByUserIfIdUserHasGuessedWord => @"
            SELECT 
                t.* 
            FROM Turn t
            INNER JOIN Round r ON t.Id = r.ActiveTurnId
            INNER JOIN Game g ON r.GameId = g.Id
            INNER JOIN GameLobby gl ON g.Id = gl.GameId
            INNER JOIN Users u ON t.UserId = u.Id AND gl.GameCode = u.GameLobbyId
            INNER JOIN ChatMessage m ON m.Message = t.Word 
                AND m.TurnId = t.Id 
                AND m.UserId = u.Id
            WHERE u.Id = @UserId";
    }
}