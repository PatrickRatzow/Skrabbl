using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.API.Services;
using Skrabbl.DataAccess;
using Skrabbl.Model;

namespace Skrabbl.API.Hubs
{
    public class ChatHub : Hub
    {
        private IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task DeleteMessage(string user, string msg)
        {
            await Clients.All.SendAsync("DeletedMessage", user, msg);
        }

        public async Task GetAllMessages(int lobbyId)
        {
            var messages = await _messageService.GetMessages(lobbyId);
            var tasks = messages.Select(msg =>
                Clients.Caller.SendAsync("ReceiveMessage", msg.User.Username, msg.Message)
            );

            await Task.WhenAll(tasks);
        }
    }
}
