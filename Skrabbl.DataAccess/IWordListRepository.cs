using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess
{
    public interface IWordListRepository
    {
        public ValueTask<IEnumerable<string>> GetAllWords();
    }
}