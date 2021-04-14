using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Skrabbl.API.Services;
using Skrabbl.DataAccess;
using Skrabbl.Model;
using Microsoft.AspNetCore.Authorization;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
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
                await Clients.All.GuessedWord(user.Result.Username);
            }
            else
            {
                await Clients.All.ReceiveMessage(user.Result.Username, message);
            }
        }

        public async Task DeleteMessage(string user, string msg)
        {
            await Clients.All.DeletedMessage(user, msg);
        }

        public async Task GetAllMessages(int lobbyId)
        {
            var messages = await _messageService.GetMessages(lobbyId);
            var tasks = messages.Select(msg =>
                Clients.Caller.ReceiveMessage(msg.User.Username, msg.Message)
            );

            await Task.WhenAll(tasks);
        }
    }
}