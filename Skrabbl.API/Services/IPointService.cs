using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Services
{
   public interface IPointService
    {
        public Dictionary<User, int> CalculatePoints(Turn turn, List<ChatMessage> messages, List<User> users);

    }
}
