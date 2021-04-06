namespace Skrabbl.DataAccess.Queries
{
    public class CommandText : ICommandText
    {
        public string GetUserById => "SELECT * FROM Users WHERE Id = @Id";
        public string AddUser =>
            "INSERT INTO Users (INSERT INTO Users(Username, Email, Password, Salt) VALUES (@Username, @Email, @Password, @Salt))";

    }
}