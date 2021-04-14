using Skrabbl.API.Services;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : IGameClient
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public GameHub(IMessageService messageService, IUserService userService, IGameService gameService)
        {
            _messageService = messageService;
            _userService = userService;
            _gameService = gameService;
        }
    }
}