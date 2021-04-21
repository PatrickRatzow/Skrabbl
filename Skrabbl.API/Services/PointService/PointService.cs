using System;
using System.Collections.Generic;
using System.Linq;
using Skrabbl.Model;

namespace Skrabbl.API.Services
{
    public class PointService : IPointService
    {
        public Dictionary<User, int> CalculatePoints(Turn turn, List<ChatMessage> messages, List<User> users)
        {
            List<ChatMessage> messageList = FilterMessagesByTime(turn, messages);
            List<ChatMessage> allCorrectGuesses = CorrectGuesses(turn, messageList);
            Dictionary<User, int> points1 = PointsToUsers(turn, allCorrectGuesses, users);
            Dictionary<User, int> points2 = PointsForWrongAnswers(turn, messageList, users);
            return MergeDictionaries(points1, points2);
        }

        public List<ChatMessage> FilterMessagesByTime(Turn turn, List<ChatMessage> messages)
        {
            // filtrer alle beskeder fra der er udenfor tidsrummet hvor turn har eksisteret - beholder alle indenfore tidsrummet
            List<ChatMessage> messageList =
                messages.FindAll(m => m.CreatedAt >= turn.StartTime && m.CreatedAt <= turn.EndTime).ToList();
            messageList.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));
            return messageList;
        }

        public List<ChatMessage> CorrectGuesses(Turn turn, List<ChatMessage> messages)
        {
            List<ChatMessage> allCorrectGuesses = messages.FindAll(m => m.Message.Equals(turn.Word));
            return allCorrectGuesses;
        }

        public Dictionary<User, int> PointsToUsers(Turn turn, List<ChatMessage> allCorrectGuesses, List<User> users)
        {
            Dictionary<User, int> points = users.ToDictionary(u => u, u => 0);

            for (int i = 0; i < allCorrectGuesses.Count; i++)
            {
                points[allCorrectGuesses[i].User] = points[allCorrectGuesses[i].User] + indexToPoint(i) +
                                                    Penalty(GetPenaltyTime(turn.StartTime,
                                                        allCorrectGuesses[i].CreatedAt));
            }

            return points;
        }

        public Dictionary<User, int> PointsForWrongAnswers(Turn turn, List<ChatMessage> messages, List<User> users)
        {
            Dictionary<User, List<ChatMessage>> dicOfUserMsg =
                messages.GroupBy(m => m.User).ToDictionary(group => group.Key, group => group.ToList());
            Dictionary<User, int> points = users.ToDictionary(u => u, u => 0);
            foreach (var item in dicOfUserMsg)
            {
                //TODO: Need all the users who havent written at all
                int penalty = GetPenaltyFromGuesses(NumberOfWrongGuesses(item.Value, turn.Word));
                points[item.Key] = penalty;
            }

            return points;
        }

        //all dictionaries sent into this method needs to have the same users as keys
        public Dictionary<User, int> MergeDictionaries(Dictionary<User, int> dic1, Dictionary<User, int> dic2)
        {
            Dictionary<User, int> mergedPoints = new Dictionary<User, int>();
            foreach (var item in dic1)
            {
                mergedPoints[item.Key] = item.Value;
            }

            foreach (var item in dic2)
            {
                mergedPoints[item.Key] = mergedPoints[item.Key] + item.Value;
            }

            return mergedPoints;
        }

        private int indexToPoint(int index)
        {
            if (index == 0)
            {
                return 1000;
            }

            if (index == 1)
            {
                return 500;
            }

            if (index == 2)
            {
                return 250;
            }

            return 100;
        }

        private int Penalty(int seconds)
        {
            if (seconds <= 20)
            {
                return 0;
            }

            if (seconds > 20 && seconds <= 40)
            {
                return -20;
            }

            if (seconds > 40 && seconds <= 60)
            {
                return -40;
            }

            return -60;
        }

        private int GetPenaltyTime(DateTime time1, DateTime time2)
        {
            TimeSpan ts = time2 - time1;
            return (int) (ts.TotalSeconds);
        }

        private int GetPenaltyFromGuesses(int wrongGuesses)
        {
            if (wrongGuesses == 0)
            {
                return 50;
            }

            if (wrongGuesses > 0 && wrongGuesses <= 5)
            {
                return -10;
            }

            if (wrongGuesses > 5 && wrongGuesses <= 10)
            {
                return -20;
            }

            return -30;
        }

        private int NumberOfWrongGuesses(List<ChatMessage> messages, string word)
        {
            return messages.FindAll(m => m.Message != word).Count();
        }
    }
}