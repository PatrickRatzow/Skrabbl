namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string AddLobby => "INSERT INTO GameLobby(GameCode, LobbyOwnerId) VALUES(@GameCode, @LobbyOwnerId)";

        public string GetLobbyByLobbyCode => "SELECT * FROM GameLobby WHERE GameCode = @GameCode";

        public string RemoveLobbyById => "DELETE FROM GameLobby WHERE GameCode = @GameCode";

        public string GetAllLobbies => "SELECT * FROM GameLobby";

        public string RemoveAllLobbies => "DELETE FROM GameLobby";
        public string GetLobbyByOwnerId => "SELECT * FROM GameLobby WHERE LobbyOwnerId = @LobbyOwnerId";
        public string GetGameSettingsByGameCode => "SELECT * FROM GameSetiing WHERE GameCode = @GameCode";
        public string SetGameSettingsByGameCode => "INSERT INTO GameSetting(GameCode, Setting, Value) VALUES(@GameCode, @Setting, @Value)";
        public string UpdateGameSettingsByGameCode => "UPDATE GameSetting SET Value = @Value WHERE GameCode = @GameCode AND Setting = @Setting";

    }
}