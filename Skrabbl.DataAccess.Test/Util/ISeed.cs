using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test
{
    internal interface ISeed
    {
        Task Up(SqlConnection conn, DbTransaction trx);
        Task Down(SqlConnection conn);
    }
}