namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetUserById => "SELECT * FROM Users WHERE Id = @Id";
        public string GetUserByUsername => "SELECT * FROM Users WHERE Username = @Username";
        public string AddUser =>
            @"
            INSERT INTO Users(Username, Email, Password, Salt) 
            VALUES (@Username, @Email, @Password, @Salt); 
            SELECT CAST(SCOPE_IDENTITY() as int)";
        public string RemoveUserById => "DELETE FROM Users WHERE id = @Id";
    }
}