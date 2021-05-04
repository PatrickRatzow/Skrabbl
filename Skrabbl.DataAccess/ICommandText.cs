namespace Skrabbl.DataAccess
{
    public interface ICommandText
    {
        string GetUserById { get; }
        string GetUserByUsername { get; }
        string GetUserByRefreshToken { get; }
        string AddUser { get; }
        string RemoveUserById { get; }
        string AddLobby { get; }
        string GetLobbyByLobbyCode { get; }
        string RemoveLobbyById { get; }
        string GetAllLobbies { get; }
        string RemoveAllLobbies { get; }
        string GetLobbyByOwnerId { get; }
        string GetAllMessages { get; }
        string GetAllMessagesByUserId { get; }
        string SaveMessage { get; }
        string RemoveAllMessages { get; }
        string GetGameById { get; }
        string AddGame { get; }
        string AddUserToLobby { get; }
        string GetAllWords { get; }
        string AddRefreshToken { get; }
        string RemoveRefreshToken { get; }
        string RemoveAllRefreshTokensForUser { get; }
        string RemoveAllExpiredRefreshTokens { get; }
        string GetCurrentTurnByUserId { get; }
        string GetCurrentTurnByUserIfIdUserHasGuessedWord { get; }
        string GetRoundStatusForGame { get; }
        string SetRoundNumberForGame { get; }
        string GetGameSettingsByGameCode { get; }
        string UpdateGameSetting { get; }
    }
}