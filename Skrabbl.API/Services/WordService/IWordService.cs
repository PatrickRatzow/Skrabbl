using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public interface IWordService
    {
        Task<IEnumerable<string>> GetNewWords(int gameId);
        public Task AddUsedWord(int gameId, string word);
    }
}