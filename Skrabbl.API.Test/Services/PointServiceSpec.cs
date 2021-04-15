using NUnit.Framework;
using Skrabbl.API.Services;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Skrabbl.API.Test.Services
{

    public class PointServiceSpec
    {
        private PointService _service;


        [SetUp]
        public void Setup()
        {
            _service = new PointService();
        }

        [Test]
        public void TestPointSystem()
        {
            int userId1 = 1;
            int userId2 = 2;
            int userId3 = 3;

            List<User> users = new List<User>();
            users.Add(CreateUser(userId1));
            users.Add(CreateUser(userId2));
            users.Add(CreateUser(userId3));



            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(100);


            Turn turn = new Turn
            {
                EndTime = endTime,
                StartTime = startTime,
                Word = new GuessWord { Word = "Hest" }
            };
            List<ChatMessage> messages = new List<ChatMessage>();


            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));


            Dictionary<User, int> point = _service.CalculatePoints(turn, messages, users);
            Debug.WriteLine(point);
            int i = 0;
        }

        private ChatMessage CreateChatMessage(int userId, DateTime time, string word)
        {
            return new ChatMessage
            {
                User = CreateUser(userId),
                CreatedAt = time,
                Message = word
            };
        }
        private User CreateUser(int userId)
        {
            return new User { Id = userId };
        }

    }
}
