using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace Skrabbl.API.Services
{
    public class WordService : IWordService
    {
        IWordListRepository _wordListRepository;
        static IEnumerable<GuessWord> words;
        static DateTime refreshTimeWords;
        //in minutes
        static int refreshInterval = 1;

        public WordService(IWordListRepository wordListRepo)
        {
            _wordListRepository = wordListRepo;
        }
        public async Task<bool> DoesWordExist(string word)
        {
            if(words == null || DateTime.Now > refreshTimeWords) { 
            words = await _wordListRepository.GetAllWords();
                refreshTimeWords = DateTime.Now.AddMinutes(refreshInterval);
            }
            return words.ToList().Any(w => w.Word == word);
            



        }
    }
}
