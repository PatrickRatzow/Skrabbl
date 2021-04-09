namespace Skrabbl.DataAccess.Queries
{
    public partial class CommandText : ICommandText
    {
        public string GetGameById => "SELECT * FROM game WHERE Id = @Id";
    }
}