using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql
{
    public class WordListRepository : BaseRepository, IWordListRepository
    {
        private readonly ICommandText _commandText;

        public WordListRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async ValueTask<IEnumerable<GuessWord>> GetAllWords()
        {
            return await WithConnection<IEnumerable<GuessWord>>(async conn =>
            {
                return await conn.QueryAsync<GuessWord>(_commandText.GetAllWords);
            });
        }
    }
}