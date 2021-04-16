using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test
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
            words  = await _wordListRepository.GetAllWords();

            //Assert
            Assert.IsNotNull(words);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }
    }
}
