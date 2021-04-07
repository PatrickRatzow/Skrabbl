namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText : ICommandText
    {
        public string AddLobby => "INSERT INTO GameLobby(GameCode, LobbyOwnerId) VALUES(@GameCode, @LobbyOwnerId)";

        public string GetLobbyById => "SELECT * FROM GameLobby WHERE GameCode = @GameCode";

        public string RemoveLobbyById => "DELETE FROM GameLobby WHERE GameCode = @GameCode";

        public string GetAllLobbies => "SELECT * FROM GameLobby";

        public string RemoveAllLobbies => "DELETE FROM GameLobby";
        
    }
}