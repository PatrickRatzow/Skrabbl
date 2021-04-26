using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Skrabbl.DataAccess.Test.Seedings
{
    [Seed(1)]
    public class CreateTestUsers : ISeed
    {
        public Task Up(SqlConnection conn, DbTransaction trx)
        {
            return conn.ExecuteAsync(@"
                INSERT INTO Users(Username, Email, Password, Salt, GameLobbyId)
                VALUES ('Patrick', 'patrick@email.dk', @Password, @Salt, NULL)
            ", new
            {
                Password = "pLNZSux4l2ar1z6PKh4tiBSZ25OSaim5R1bmXuD+aS8=",
                Salt = "mvV8K4PoKh41psKjxAWTGQ=="
            }, trx);
        }

        public Task Down(SqlConnection conn)
        {
            return conn.ExecuteAsync("DELETE FROM Users");
        }
    }
}