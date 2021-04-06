using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Skrabbl.DataAccess.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Test
{
    class GameLobbyRepositorySpec
    {
        IGameLobbyRepository gameLobbyRepository;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigFixture().Config;
            var cmd = new CommandText();

            gameLobbyRepository = new GameLobbyRepository(config, cmd);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
