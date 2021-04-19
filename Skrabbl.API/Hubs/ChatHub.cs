using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Skrabbl.API.Hubs
{
    [Authorize]
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        public async Task SendMessage(string message)
        {
            var userName = Context.User.Identity.Name;
            var idClaim = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null || userName == null) return;
            var userId = int.Parse(idClaim.Value);

            var gameId = 5;
            /*
            var user = await _userService.GetUser(userId);
            var game = _gameService.GetGame(user.GameLobbyId);

            await Task.WhenAll(user, game);

            if (user.Result == null || game.Result == null)
                return;
            */
            await _messageService.CreateMessage(message, gameId, userId);
            bool wordExist = await _wordService.DoesWordExist(message);

            if (wordExist)
            {
                await Clients.All.GuessedWord(userName);
            }
            else
            {
                await Clients.All.ReceiveMessage(userName, message);
            }
        }

        public async Task DeleteMessage(string user, string msg)
        {
            await Clients.All.DeletedMessage(user, msg);
        }

        public async Task GetAllMessages()
        {
            var idClaim = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (idClaim == null) return;
            var userId = int.Parse(idClaim.Value);

            var messages = await _messageService.GetMessagesByUserId(userId);
            var tasks = messages.Select(msg =>
                Clients.Caller.ReceiveMessage(msg.User.Username, msg.Message)
            );

            await Task.WhenAll(tasks);
        }
    }
}