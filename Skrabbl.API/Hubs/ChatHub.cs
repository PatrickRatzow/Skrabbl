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

            var clients = Clients.All;

            var user = _userService.GetUser(userId);
            var hasGuessedWord = _gameService.HasUserGuessedWord(userId);

            await Task.WhenAll(user, hasGuessedWord);

            if (user.Result.GameLobbyId == null || hasGuessedWord.Result) return;

            var guessedWord = _gameService.DidUserGuessWord(userId, message);

            await Task.WhenAll(
                _messageService.CreateMessage(message, userId),
                guessedWord
            );

            if (guessedWord.Result)
            {
                await Clients.All.GuessedWord(userName);

                return;
            }

            await Clients.All.ReceiveMessage(userName, message);
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