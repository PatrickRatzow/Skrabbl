namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText : ICommandText
    {
        public string AddRefreshToken => @"
            INSERT INTO UserRefreshToken(Token, ExpiresAt, UserId)
            VALUES (@Token, @ExpiresAt, @UserId)";

        public string RemoveRefreshToken => @"
            DELETE FROM UserRefreshToken
            WHERE Token = @Token
                AND ExpiresAt > CURRENT_TIMESTAMP;";

        public string RemoveAllRefreshTokensForUser => @"DELETE FROM UserRefreshToken WHERE UserId = @UserId";

        public string RemoveAllExpiredRefreshTokens =>
            @"DELETE FROM UserRefreshToken WHERE ExpiresAt < CURRENT_TIMESTAMP;";
    }
}