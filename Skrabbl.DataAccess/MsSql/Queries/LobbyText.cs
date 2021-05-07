namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string AddLobby => "INSERT INTO GameLobby(Code, LobbyOwnerId) VALUES(@GameCode, @LobbyOwnerId)";

        public string GetLobbyByLobbyCode => "SELECT * FROM GameLobby WHERE Code = @GameCode";

        public string RemoveLobbyById => "DELETE FROM GameLobby WHERE Code = @GameCode";

        public string GetAllLobbies => "SELECT * FROM GameLobby";

        public string RemoveAllLobbies => "DELETE FROM GameLobby";
        public string GetLobbyByOwnerId => "SELECT * FROM GameLobby WHERE LobbyOwnerId = @LobbyOwnerId";
        public string GetGameSettingsByGameCode => "SELECT * FROM GameSetting WHERE GameLobbyCode = @GameCode";

        public string SetGameSettingsByGameCode =>
            "INSERT INTO GameSetting(Code, SettingType, Value) VALUES(@GameCode, @Setting, @Value)";

        public string UpdateGameSetting => @"
            UPDATE GameSetting 
            SET Value = @Value
            WHERE SettingType = @Setting
              AND Code = (
                  SELECT Code
                  FROM GameLobby
                  WHERE LobbyOwnerId = @LobbyOwnerId
              )";
    }
}