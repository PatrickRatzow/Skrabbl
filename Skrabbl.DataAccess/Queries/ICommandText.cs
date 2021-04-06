namespace Skrabbl.DataAccess.Queries
{
    public interface ICommandText
    {
        string GetUserById { get; }
        string AddUser { get; }
        string RemoveUserById { get; }
        string AddLobby { get; }
        string GetLobbyById { get; }
        string RemoveLobbyById { get; }
        string GetAllLobbies { get; }
        string RemoveAllLobbies { get; }
    }
}