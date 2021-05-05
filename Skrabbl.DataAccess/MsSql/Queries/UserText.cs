namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetUserById => "SELECT * FROM Users WHERE Id = @Id";
        public string GetUserByUsername => "SELECT * FROM Users WHERE Username = @Username";

        public string GetUserByRefreshToken => @"
            SELECT u.* FROM Users u
            INNER JOIN UserRefreshToken urt ON u.Id = urt.UserId
            WHERE urt.Token = @Token";

        public string AddUser =>
            @"
            INSERT INTO Users(Username, Email, Password, Salt) 
            VALUES (@Username, @Email, @Password, @Salt); 
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public string RemoveUserById => "DELETE FROM Users WHERE id = @Id";
        public string AddUserToLobby => "UPDATE Users SET GameLobbyId = @GameLobbyId WHERE Id = @Id";
        public string GetUsersByGameCode => "SELECT GameLobbyId from Users WHERE GameLobbyId = @GameLobbyid";
    }
}