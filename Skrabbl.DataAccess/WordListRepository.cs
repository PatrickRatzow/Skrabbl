using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
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