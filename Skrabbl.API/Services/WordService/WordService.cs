using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Skrabbl.DataAccess;

namespace Skrabbl.API.Services
{
    public class WordService : IWordService
    {
        private readonly IWordListRepository _wordListRepository;
        private readonly IMemoryCache _memoryCache;

        public WordService(IWordListRepository wordListRepo, IMemoryCache memoryCache)
        {
            _wordListRepository = wordListRepo;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<string>> GetNewWords(int gameId)
        {
            var wordList = GetWordList();
            var usedWords = GetUsedWords(gameId);

            await Task.WhenAll(wordList, usedWords);

            var random = new Random();
            return wordList.Result
                .Except(usedWords.Result)
                .OrderBy(x => random.Next())
                .Take(3);
        }

        public async Task AddUsedWord(int gameId, string word)
        {
            var usedWords = await GetUsedWords(gameId);
            usedWords.Add(word);

            _memoryCache.Set(UsedWordCacheKey(gameId), usedWords, TimeSpan.FromMinutes(10));
        }

        private string UsedWordCacheKey(int gameId) => $"usedWords:{gameId}";

        private Task<HashSet<string>> GetUsedWords(int gameId)
        {
            return _memoryCache.GetOrCreateAsync(UsedWordCacheKey(gameId), async (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

                return new HashSet<string>();
            });
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