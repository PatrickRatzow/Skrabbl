using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test;

namespace Skrabbl.DataAccess.MsSql.Test
{
    class WordListRepositorySpec
    {
        IWordListRepository _wordListRepository;

        [SetUp]
        public void Setup()
        {
            var cmd = new CommandText();

            _wordListRepository = new WordListRepository(ConfigFixture.Config, cmd);
        }

        [Test]
        public async Task GetAllWordsFromDb()
        {
            //Arrange
            IEnumerable<string> words;
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