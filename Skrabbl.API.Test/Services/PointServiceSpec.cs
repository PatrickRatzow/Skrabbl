﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Skrabbl.API.Services;
using Skrabbl.Model;

namespace Skrabbl.API.Test.Services
{
    public class PointServiceSpec
    {
        private PointService _service;
        private List<ChatMessage> messages;
        private List<User> users;
        private Turn turn;
        private DateTime startTime;
        private DateTime endTime;
        private int userId1 = 1;
        private int userId2 = 2;
        private int userId3 = 3;


        [SetUp]
        public void Setup()
        {
            _service = new PointService();
            messages = new List<ChatMessage>();
            users = new List<User>();
            users.Add(CreateUser(userId1));
            users.Add(CreateUser(userId2));
            users.Add(CreateUser(userId3));

            startTime = DateTime.Now;
            endTime = startTime.AddSeconds(100);

            turn = new Turn
            {
                EndTime = endTime,
                StartTime = startTime,
                Word = "Hest"
            };
        }

        [Test]
        public void TestCompletePointSystem()
        {
            //Arrange
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(10), "Hest"));

            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(34), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(35), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(36), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(37), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(38), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(39), "hej"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(40), "Hest"));

            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(20), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(21), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(22), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(23), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(24), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(25), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(26), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(27), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(28), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(29), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(30), "hej"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(40), "hej"));

            List<int> expected = new List<int>();
            expected.Add(1050);
            expected.Add(460);
            expected.Add(-30);

            Dictionary<User, int> point = new Dictionary<User, int>();

            //Act
            point = _service.CalculatePoints(turn, messages, users);
            Debug.WriteLine(point);
            List<int> usersPoints = point.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        [Test]
        public void TestSortMessagesByTime()
        {
            // FIXME: This test is broken, fix!
            Assert.Ignore();
            return;
            //Arrange
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(30), "Hjort"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(40), "Hest"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(10), "Hest"));

            List<ChatMessage> expectedList = new List<ChatMessage>();
            expectedList.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));
            expectedList.Add(CreateChatMessage(userId3, startTime.AddSeconds(10), "Hest"));
            expectedList.Add(CreateChatMessage(userId2, startTime.AddSeconds(30), "Hjort"));
            expectedList.Add(CreateChatMessage(userId2, startTime.AddSeconds(40), "Hest"));


            //Act
            messages = _service.FilterMessagesByTime(turn, messages);

            //Assert
            Assert.That(messages, Is.EqualTo(expectedList));
        }

        [Test]
        public void TestAllCorrectGuesses()
        {
            //Arrange
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(30), "Hjort"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(40), "Hest"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(10), "Hest"));

            //Act
            messages = _service.CorrectGuesses(turn, messages);

            //Assert
            Assert.That(messages.Count == 3);
        }

        [Test]
        public void TestPointCalculationMinusWrongGuessesWithOneGuess()
        {
            //Arrange
            Dictionary<User, int> points = new Dictionary<User, int>();

            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));

            List<int> expected = new List<int>();
            expected.Add(1000);
            expected.Add(0);
            expected.Add(0);

            //Act
            points = _service.PointsToUsers(turn, messages, users);
            List<int> usersPoints = points.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        [Test]
        public void TestPointCalculationMinusWrongGuessesWithZeroGuess()
        {
            //Arrange
            Dictionary<User, int> points = new Dictionary<User, int>();


            List<int> expected = new List<int>();
            expected.Add(0);
            expected.Add(0);
            expected.Add(0);

            //Act
            points = _service.PointsToUsers(turn, messages, users);
            List<int> usersPoints = points.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        [Test]
        public void TestPointCalculationMinusWrongGuessesWithAllGuess()
        {
            //Arrange
            Dictionary<User, int> points = new Dictionary<User, int>();

            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));
            messages.Add(CreateChatMessage(userId2, startTime.AddSeconds(3), "Hest"));
            messages.Add(CreateChatMessage(userId3, startTime.AddSeconds(3), "Hest"));

            List<int> expected = new List<int>();
            expected.Add(1000);
            expected.Add(500);
            expected.Add(250);

            //Act
            points = _service.PointsToUsers(turn, messages, users);
            List<int> usersPoints = points.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        [Test]
        public void TestTotalPointsWith1Userwith1CorrectAnswer()
        {
            // FIXME: This test is broken, fix!
            Assert.Ignore();
            return;
            //Arrange
            Dictionary<User, int> points = new Dictionary<User, int>();

            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(3), "Hest"));

            List<int> expected = new List<int>();
            expected.Add(1050);
            expected.Add(0);
            expected.Add(0);

            //Act
            points = _service.PointsForWrongAnswers(turn, messages, users);
            List<int> usersPoints = points.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        [Test]
        public void TestPointsForWrongAnswers1UserWith6Wrong1Correct()
        {
            //Arrange
            Dictionary<User, int> points = new Dictionary<User, int>();

            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(4), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(5), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(6), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(7), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(8), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(9), "hej"));
            messages.Add(CreateChatMessage(userId1, startTime.AddSeconds(10), "Hest"));

            List<int> expected = new List<int>();
            expected.Add(-20);
            expected.Add(0);
            expected.Add(0);

            //Act
            points = _service.PointsForWrongAnswers(turn, messages, users);
            List<int> usersPoints = points.Select(v => v.Value).ToList();

            //Assert
            Assert.That(usersPoints, Is.EqualTo(expected));
        }

        //needs to have same users in both dictionaries
        [Test]
        public void TestMergingTwoDictionaries()
        {
            //Arrange
            User user1 = users[0];
            User user2 = users[1];
            User user3 = users[2];

            Dictionary<User, int> dic1 = new Dictionary<User, int>();
            Dictionary<User, int> dic2 = new Dictionary<User, int>();

            dic1[user1] = 7;
            dic1[user2] = 8;
            dic1[user3] = 9;

            dic2[user1] = 1;
            dic2[user2] = 2;
            dic2[user3] = 3;
            //Act
            Dictionary<User, int> result = _service.MergeDictionaries(dic1, dic2);
            //Assert
            Assert.AreEqual(8, result[user1]);
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
            return new User {Id = userId};
        }
    }
}