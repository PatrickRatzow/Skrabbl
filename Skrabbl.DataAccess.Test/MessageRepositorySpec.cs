using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Skrabbl.DataAccess.MsSql.Queries;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.MsSql.Test
{
    class MessageRepositorySpec
    {
        IMessageRepository _messageRepository;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigFixture().Config;
            var cmd = new CommandText();

            _messageRepository = new MessageRepository(config, cmd);
        }

        [Test]
        public async Task GetAllMessagesFromDbTest()
        {
            //Arrange
            IEnumerable<ChatMessage> msgList;
            int gamelobbyId = 10;

            //Act
            msgList = await _messageRepository.GetAllMessages(gamelobbyId);

            //Assert
            Assert.IsNotNull(msgList);
        }

        [Test]
        public async Task AddChatMessageToDbTest()
        {
            //Arrange
            DateTime date = new DateTime(2012, 12, 25, 10, 30, 50);

            ChatMessage chatMessage = new ChatMessage
            {
                Message = "hej",
                CreatedAt = date,
                Game = new Game {Id = 3},
                User = new User {Id = 25}
            };

            //Act
            await _messageRepository.SaveMessage(chatMessage);
            IEnumerable<ChatMessage> chatMsg = await _messageRepository.GetAllMessages(1223);

            //Assert
            IList<ChatMessage> list = new List<ChatMessage>(chatMsg);

            Assert.AreNotEqual(list.Count, 0);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _messageRepository.RemoveAllChatMessages();
        }
    }
}