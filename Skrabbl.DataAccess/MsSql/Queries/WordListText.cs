namespace Skrabbl.DataAccess.MsSql.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetAllWords => "SELECT * FROM WordList";
    }
}