using System;
using NUnit.Framework;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Skrabbl.API.Services;
using Skrabbl.Model.Errors;
using NUnit.Framework.Constraints;

namespace Skrabbl.API.Test.Services
{
    class MessageServiceSpec
    {
        [Test]
        public async Task CreateMessage_Succeeds()
        {
            //Arrange
            string msg = "Test msg";
            int gameId = 3;
            int userId = 25;
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.SaveMessage(It.IsAny<ChatMessage>())).Returns(Task.FromResult<ChatMessage>(null));
            var service = new MessageService(mock.Object);

            //Act
            await service.CreateMessage(msg, userId, gameId);

            //Assert
            mock.VerifyAll();
        }

        [Test]
        public async Task GetAllMessages_Succeeds()
        {
            //Arrange
            int gameId = 3;
            var mock = new Mock<IMessageRepository>();
            mock.Setup(m => m.GetAllMessages(It.IsAny<int>())).Returns(Task.FromResult<IEnumerable<ChatMessage>>(null)); 
            var service = new MessageService(mock.Object);

            //Act
            await service.GetMessages(gameId);

            //Assert
            mock.VerifyAll();
        }
    }
}
