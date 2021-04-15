using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
    public class PointService : IPointService
    {
        public Dictionary<User, int> CalculatePoints(Turn turn, List<ChatMessage> messages, List<User> users)
        {
            // alle messageList er alle messages fra et game
            // filtrer alle beskeder fra der er udenfor tidsrummet hvor turn har eksisteret - beholder alle indenfore tidsrummet
            List<ChatMessage> messageList = messages.FindAll(m => m.CreatedAt >= turn.StartTime && m.CreatedAt <= turn.EndTime).ToList();
            // Her sortere den dem i tid (ikke sikker på at den gør det rigtig - x og y skal måske byttes)
            messageList.Sort((x, y) => x.CreatedAt.CompareTo(y.CreatedAt));
            // her bliver der fundet alle korrekte svar udfra turn."currentword" og message
            List<ChatMessage> allCorrectGuesses = messageList.FindAll(m => m.Message.Equals(turn.Word.Word));
            // Dict create til at holde data 
            Dictionary<User, int> points = users.ToDictionary(u => u, u => 0);
            // Løber igennem alle de sorteret korrekte svar. Tager useren udfra index og efter giver point til den user i forhold til indexet efter plusser vi
            // penalty til ved differencen af beskedens start og turens start
            for (int i = 0; i < allCorrectGuesses.Count; i++)
            {
                
                points[allCorrectGuesses[i].User] = points[allCorrectGuesses[i].User] + indexToPoint(i) + Penalty(GetPenaltyTime(turn.StartTime, allCorrectGuesses[i].CreatedAt));
                //points.Add(allCorrectGuesses[i].User, indexToPoint(i) + Penalty(GetPenaltyTime(turn.StartTime, allCorrectGuesses[i].CreatedAt)));
            }
            //en dictionary hvor vi gemmer User som key og list<ChatMessage> fra den person i en dictionary VHA grp by
            Dictionary<User, List<ChatMessage>> dicOfUserMsg = messageList.GroupBy(m => m.User).ToDictionary(group => group.Key, group => group.ToList());
            //for hver item i dic skal vi gette penalty for antallet af wrongguesses. Der bliver 
            foreach (var item in dicOfUserMsg)
            {

               int penalty = GetPenaltyFromGuesses(NumberOfWrongGuesses(item.Value, turn.Word.Word));
                //TODO: Need all the users who havent written at all
                points[item.Key] = points[item.Key] + penalty;
            }
            return points;
            // Point baseret på placering (1st place = 1000p 2nd place = 500 3rd place = 250p 4 > place X 100p
            // Point baseret på hurtighed i tid... 20sekunder = -50
            //
            //
            //
            //
            // 1000 +
            //objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));
        }

        private int indexToPoint(int index)
        {
            if (index == 0)
            {
                return 1000;
            }
            if (index == 2)
            {
                return 500;
            }
            if (index == 3)
            {
                return 250;
            }

            return 100;
        }

        private int Penalty(int seconds) 
        {
            if (seconds <= 20) {
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
            return (int)(ts.TotalSeconds);
                
            
        }

        private int GetPenaltyFromGuesses(int wrongGuesses) 
        {
            if (wrongGuesses == 0)
            {
                return 50;
            }
            if (wrongGuesses > 0 && wrongGuesses <= 5) {
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

