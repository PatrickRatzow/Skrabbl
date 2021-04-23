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

        public string GetRoundStatusForGame => @"
            SELECT
                COUNT(*) AS NumberOfRounds,
                (
                    SELECT
                        r2.RoundNumber 
                    FROM Round r2
                    INNER JOIN Game g ON r2.GameId = g.Id AND r2.Id = g.ActiveRoundId
                    WHERE r2.GameId = @GameId
                ) AS ActiveRoundNumber
            FROM Round r
            WHERE r.GameId = @GameId";

        public string SetRoundNumberForGame => @"
            UPDATE Game
            SET ActiveRoundId = (
                SELECT 
                    Id
                FROM Round 
                WHERE RoundNumber = @RoundNumber
                  AND GameId = @GameId
            )
            WHERE Id = @GameId";
    }
}