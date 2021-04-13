using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public class WordService : IWordService
    {
        IWordListRepository _wordListRepository;

        public WordService(IWordListRepository wordListRepo)
        {
            _wordListRepository = wordListRepo;
        }
        public async Task<bool> DoesWordExist(string word)
        {
            var words = await _wordListRepository.GetAllWords();
            return words.ToList().Any(w => w.Word == word);
            
           
            
        }
    }
}
