using Microsoft.AspNetCore.SignalR;
using Skrabbl.API.Services;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        private readonly IGameLobbyService _gameLobbyService;
        private readonly IGameService _gameService;
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IWordService _wordService;

        public GameHub(IMessageService messageService, IUserService userService, IGameService gameService,
            IGameLobbyService gameLobbyService,IWordService wordService )
        {
            _messageService = messageService;
            _userService = userService;
            _gameService = gameService;
            _gameLobbyService = gameLobbyService;
            _wordService = wordService;
            
        }
    }
}