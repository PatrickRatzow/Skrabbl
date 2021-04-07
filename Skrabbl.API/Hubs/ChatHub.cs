using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Hubs
{
    public class ChatHub : Hub
    {
        private MessageRepository messageRepository;

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task DeleteMessage(string user, string msg)
        {
            await Clients.All.SendAsync("DeletedMessage", user, msg);
        }

        public async Task GetMessages(string user, string msg)
        {
            await Clients.All.SendAsync("GetMessages", user, msg);
        }
    }
}
