using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Skrabbl.DataAccess.Test
{
    public abstract class Seed : ISeed
    {
        public IDbConnection Connection { private get; set; }
        public IDbTransaction Transaction { private get; set; }

        public abstract Task Up();

        public abstract Task Down();

        protected Task Execute(string query, object param = null)
        {
            return Connection.QueryAsync(query, param, Transaction);
        }
    }
}