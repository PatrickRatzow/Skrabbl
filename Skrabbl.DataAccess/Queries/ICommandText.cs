namespace Skrabbl.DataAccess.Queries
{
    public interface ICommandText
    {
        string GetUserById { get; }
        string AddUser { get; }
    }
}