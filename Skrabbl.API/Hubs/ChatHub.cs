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
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly IWordService _wordService;


        public ChatHub(IMessageService messageService, IUserService userService, IGameService gameService, IWordService wordService)
        {
            _messageService = messageService;
            _userService = userService;
            _gameService = gameService;
            _wordService = wordService;
        }
        
        public async Task SendMessage(int gameId, int userId, string message)
        {
            var user = _userService.GetUser(userId);
            var game = _gameService.GetGame(gameId);
        
            await Task.WhenAll(user, game);

            if (user.Result == null || game.Result == null)
                return;

            await _messageService.CreateMessage(message, gameId, userId);

            bool wordExist = await _wordService.DoesWordExist(message);

            if (wordExist)
            {
                await Clients.All.SendAsync("GuessedWord", user.Result.Username);
            } else
            {
                await Clients.All.SendAsync("ReceiveMessage", user.Result.Username, message);
            }

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
