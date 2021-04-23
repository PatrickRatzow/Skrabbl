using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Skrabbl.API.Services.Cache;
using Skrabbl.DataAccess;

namespace Skrabbl.API.Services
{
    public class WordService : IWordService
    {
        private readonly IWordListRepository _wordListRepository;
        private readonly HashSet<string> _usedWords = new HashSet<string>();
        private readonly IMemoryCache _memoryCache;

        public WordService(IWordListRepository wordListRepo, IMemoryCache memoryCache)
        {
            _wordListRepository = wordListRepo;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<string>> GetNewWords()
        {
            IEnumerable<string> wordList = await GetWordList();

            var random = new Random();
            return wordList
                .Except(_usedWords)
                .OrderBy(x => random.Next())
                .Take(3);
        }

        public void AddUsedWord(string word)
        {
            _usedWords.Add(word);
        }

        private Task<IEnumerable<string>> GetWordList()
        {
            return _memoryCache.GetOrCreateAsync(CachedKeys.AllWords, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                entry.SlidingExpiration = TimeSpan.FromMinutes(2);

                return await _wordListRepository.GetAllWords();
            });
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