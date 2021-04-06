namespace Skrabbl.DataAccess.Queries
{
    public interface ICommandText
    {
        string GetUserById { get; }
        string AddUser { get; }
        string RemoveUserById { get; }
        string GetAllMessages { get; }
        string SaveMessage { get; }
    }
}