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

        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public User User { get; private set; }
        public Game Game { get; private set; }

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
            ChatMessage chatMessage = new ChatMessage();
            {

                Message = "hej";
                CreatedAt = DateTime.Now;
                User = new User { Id = 25 };
                Game = new Game { Id = 1 };
                
                //1223
            };

            //Act
           // await _messageRepository.SaveMessage(chatMessage);
            //GameLobby lobby = await _gameLobbyRepository.GetGameLobbyById(gameLobby.GameCode);

            //Assert
           // Assert.IsNotNull(lobby);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
          //  _gameLobbyRepository.RemoveAllGameLobbies();
        }
    }
}
