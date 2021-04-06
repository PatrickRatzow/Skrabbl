using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.DataAccess;

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

        public List<Message> GetGameMessagesForGameLobby(string gameLobbyId) {
            messageRepository = new MessageRepository();
            return messageRepository.GetAllMessages(gameLobbyId);

            
        }
       
    }
}
