using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [TestFixture]
    [Order(1)]
    class MessageRepositorySpec
    {
        private IMessageRepository _messageRepository;
        private IGameRepository _gameRepository;

        [SetUp]
        public void Setup()
        {
            var cmd = new CommandText();

            _messageRepository = new MessageRepository(ConfigFixture.Config, cmd);
            _gameRepository = new GameRepository(ConfigFixture.Config, cmd);
        }

        [Test]
        [Order(1)]
        public async Task GetMessagesByUserIdTest()
        {
            // Arrange
            var turn = await _gameRepository.GetCurrentTurn(TestData.Users.Patrick.Id);
            // This query does not have the joins for all chat messages to be in the object
            var expectedMessages = TestData.Games.PatrickGame.Rounds
                .Select(r => r.Turns.First(t => t.Id == turn.Id))
                .First()
                .Messages.Count;

            //Act            
            var messages = await _messageRepository.GetAllMessagesByUserId(TestData.Users.Patrick.Id);

            //Assert
            Assert.AreEqual(messages.Count(), expectedMessages);
        }

        [Test]
        [Order(2)]
        public async Task AddChatMessageToDbTest()
        {
            //Arrange
            var chatMessage = new ChatMessage
            {
                Message = "Test Message 1",
                CreatedAt = DateTime.UtcNow,
                User = TestData.Users.Patrick
            };

            //Act
            var messagesBefore = (await _messageRepository.GetAllMessagesByUserId(TestData.Users.Patrick.Id))
                .ToList();
            await _messageRepository.SaveMessage(chatMessage);
            var messagesAfter = (await _messageRepository.GetAllMessagesByUserId(TestData.Users.Patrick.Id))
                .ToList();

            //Assert
            Assert.AreNotEqual(messagesBefore.Count, messagesAfter.Count);
            Assert.AreEqual(messagesBefore.Count + 1, messagesAfter.Count);
        }
    }
}