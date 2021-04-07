using Microsoft.Azure.Amqp.Framing;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using Skrabbl.DataAccess.Queries;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Skrabbl.DataAccess.Test
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
        public void Test1()
        {
            Assert.Pass();
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
                Game = new Game { Id = 3 },
                User = new User { Id = 25 }
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
