namespace Skrabbl.DataAccess.Queries
{
    public interface ICommandText
    {
        string GetUserById { get; }
        string GetUserByUsername { get; }
        string AddUser { get; }
        string RemoveUserById { get; }
        string AddLobby { get; }
        string GetLobbyById { get; }
        string RemoveLobbyById { get; }
        string GetAllLobbies { get; }
        string RemoveAllLobbies { get; }
        string GetAllMessages { get; }
        string SaveMessage { get; }
        string RemoveAllMessages { get; }
    }
}