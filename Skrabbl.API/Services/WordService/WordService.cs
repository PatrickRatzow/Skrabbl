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
        static HashSet<GuessWord> usedWords = new HashSet<GuessWord>();
        static DateTime refreshTimeWords;
        //in minutes
        static int refreshInterval = 1;

        public WordService(IWordListRepository wordListRepo)
        {
            _wordListRepository = wordListRepo;
        }
        public async Task<bool> DoesWordExist(string word)
        {
            IEnumerable<GuessWord> words = await CachesWordList();
           
            return words.Any(w => w.Word == word);
        }



        public async Task<IEnumerable<GuessWord>> GetNewWords()
        {
                IEnumerable<GuessWord> cache = await CachesWordList();
                var wordList = cache.Except(usedWords).ToList();
                Random random = new Random();
                var shuffledList = wordList.OrderBy(x => random.Next()).ToList();
                return shuffledList.GetRange(0, 3);
        }

        public Task<bool> UsedWords(string word)
        {
            return Task.Run(() =>
            {
                //FejlHåndtering find ud af om det ord der bliver sendt videre findes i den cached liste
                usedWords.Add(new GuessWord { Word = word });
                return true;
            });

        }

        private async Task<IEnumerable<GuessWord>> CachesWordList() 
        {
            Debug.WriteLine("usedWords " + usedWords);
            if (words == null || DateTime.Now > refreshTimeWords)
            {
                words = await _wordListRepository.GetAllWords();
                refreshTimeWords = DateTime.Now.AddMinutes(refreshInterval);
            }
            return words.ToList();
        }
    }

}
