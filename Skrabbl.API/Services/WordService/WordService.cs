using Microsoft.Extensions.Caching.Memory;
using Skrabbl.API.Services.Cache;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Skrabbl.API.Services
{
    public class WordService : IWordService
    {
        IWordListRepository _wordListRepository;
        IEnumerable<GuessWord> words;
        HashSet<GuessWord> usedWords = new HashSet<GuessWord>();
        private readonly IMemoryCache _memoryCache;

        public WordService(IWordListRepository wordListRepo, IMemoryCache memoryCache)
        {
            _wordListRepository = wordListRepo;
            _memoryCache = memoryCache;
        }
        public async Task<bool> DoesWordExist(string word)
        {
            IEnumerable<GuessWord> words = await WordList();

            return words.Any(w => w.Word == word);
        }



        public async Task<IEnumerable<GuessWord>> GetNewWords()
        {
            Debug.WriteLine(_memoryCache.GetHashCode());
            IEnumerable<GuessWord> list = await WordList();
            var wordList = list.Except(usedWords).ToList();
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


        private async Task<IEnumerable<GuessWord>> WordList()
        {
            Debug.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var cacheEntry = await _memoryCache.GetOrCreateAsync(CachedKeys.AllWords, async entry =>
            {

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);
                IEnumerable<GuessWord> words = await _wordListRepository.GetAllWords();
                List<GuessWord> allWordsList = words.ToList();
                return allWordsList;

            });
            return cacheEntry;
        }
        // en anden måde at implementere cache på
        /* private async Task<IEnumerable<GuessWord>> WordList1()
        {
            var cacheKey = "allWordsList";
            if (!_memoryCache.TryGetValue(cacheKey, out List<GuessWord> allWordsList))
            {
                IEnumerable<GuessWord> words = await _wordListRepository.GetAllWords();
                allWordsList = words.ToList();


                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                    //Here we can set the priority of keeping the cache entry in the cache.
                    //Priority = CacheItemPriority.High
                    SlidingExpiration = TimeSpan.FromMinutes(1),
                };
                _memoryCache.Set(cacheKey, allWordsList, cacheExpiryOptions);

            }
            Debug.WriteLine(this.GetHashCode());

            return allWordsList;
        }*/
    }

}
