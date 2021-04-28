using System.Collections.Generic;
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

        protected Task<IEnumerable<dynamic>> Execute(string query, object param = null)
        {
            return Connection.QueryAsync(query, param, Transaction);
        }

        protected Task<IEnumerable<T>> Execute<T>(string query, object param = null)
        {
            return Connection.QueryAsync<T>(query, param, Transaction);
        }

        protected Task<dynamic> QueryFirst(string query, object param = null)
        {
            return Connection.QueryFirstAsync(query, param, Transaction);
        }

        protected Task<T> QueryFirst<T>(string query, object param = null)
        {
            return Connection.QueryFirstAsync<T>(query, param, Transaction);
        }

        protected Task<dynamic> QuerySingle(string query, object param = null)
        {
            return Connection.QuerySingleAsync(query, param, Transaction);
        }

        protected Task<T> QuerySingle<T>(string query, object param = null)
        {
            return Connection.QuerySingleAsync<T>(query, param, Transaction);
        }
    }
}