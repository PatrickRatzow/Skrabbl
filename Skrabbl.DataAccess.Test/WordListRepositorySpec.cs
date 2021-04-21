using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql.Test
{
    class WordListRepositorySpec
    {
        IWordListRepository _wordListRepository;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigFixture().Config;
            var cmd = new CommandText();

            _wordListRepository = new WordListRepository(config, cmd);
        }

        [Test]
        public async Task GetAllWordsFromDb()
        {
            //Arrange
            IEnumerable<GuessWord> words;
            //Act
            words = await _wordListRepository.GetAllWords();

            //Assert
            Assert.IsNotNull(words);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }
    }
}