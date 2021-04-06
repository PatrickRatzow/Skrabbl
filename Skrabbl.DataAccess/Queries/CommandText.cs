namespace Skrabbl.DataAccess.Queries
{
    public class CommandText : ICommandText
    {
        public string GetUserById => "SELECT * FROM Users WHERE Id = @Id";
        public string AddUser =>
            @"
            INSERT INTO Users(Username, Email, Password, Salt) 
            VALUES (@Username, @Email, @Password, @Salt); 
            SELECT CAST(SCOPE_IDENTITY() as int)";
        public string RemoveUserById => "DELETE FROM Users WHERE id = @Id";

        public string AddLobby => "INSERT INTO GameLobby(GameCode, LobbyOwnerId) VALUES(@GameCode, @LobbyOwnerId)";

        public string GetLobbyById => "SELECT * FROM GameLobby WHERE GameCode = @GameCode";

        public string RemoveLobbyById => "DELETE FROM GameLobby WHERE GameCode = @GameCode";

        public string GetAllLobbies => "SELECT * FROM GameLobby";

        public string RemoveAllLobbies => "DELETE FROM GameLobby";
        public string GetAllMessages => "Select * from Message";
        public string SaveMessage => "insert into ChatMessage(message, createdAt, gameId, userId) Values @messageTemp, @createdAtTemp, @gameIdTemp, @userIdTemp)";
    }
}