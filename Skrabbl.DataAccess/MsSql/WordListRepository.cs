using Dapper;
using Microsoft.Extensions.Configuration;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId); 
            return await WithConnection<IEnumerable<GuessWord>>(async conn =>
            {
                return await conn.QueryAsync<GuessWord>(_commandText.GetAllWords);
            });
        }
    }
}