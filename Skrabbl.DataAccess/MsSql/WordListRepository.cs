using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Skrabbl.DataAccess.MsSql
{
    public class WordListRepository : BaseRepository, IWordListRepository
    {
        private readonly ICommandText _commandText;

        public WordListRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async ValueTask<IEnumerable<string>> GetAllWords()
        {
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<string>(_commandText.GetAllWords);
            });
        }
    }
}