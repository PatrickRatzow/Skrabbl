namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetUserById => "SELECT * FROM [User] WHERE Id = @Id";
        public string GetUserByUsername => "SELECT * FROM [User] WHERE Username = @Username";

        public string GetUserByRefreshToken => @"
            SELECT u.* FROM Users u
            INNER JOIN UserRefreshToken urt ON u.Id = urt.UserId
            WHERE urt.Token = @Token";

        public string AddUser =>
            @"
            INSERT INTO [User](Username, Email, Password, Salt) 
            VALUES (@Username, @Email, @Password, @Salt); 
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public string RemoveUserById => "DELETE FROM [User] WHERE id = @Id";
        public string AddUserToLobby => "UPDATE [User] SET LobbyCode = @LobbyCode WHERE Id = @Id";
        public string GetUsersByGameCode => "SELECT GameLobbyId from Users WHERE GameLobbyId = @GameLobbyid";
    }
}