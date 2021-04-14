using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        public async Task ChooseWord(int gameId, string chosenWord)
        {
            int userId = 25;
            //TODO: Check if it is the users turn


        }
    }
}
