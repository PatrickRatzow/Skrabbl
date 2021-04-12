using Microsoft.AspNetCore.SignalR;
using Skrabbl.API.Services;
using Skrabbl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Hubs
{
    public class ConnectToLobbyHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IGameLobbyService _gameLobbyService;

        public ConnectToLobbyHub(IUserService userService, IGameLobbyService gameService)
        {
            _userService = userService;
            _gameLobbyService = gameService;
        }
        public async Task JoinLobby(int userId, string gameCode)
        {
            User user = await _userService.GetUser(userId);
            GameLobby gameLobby = await _gameLobbyService.GetGameLobbyById(gameCode);

            if (user == null || user.GameLobbyId != null || gameLobby == null)
                return;


            //Go to database and change the players connected to lobby + player connected lobby
            await _userService.AddToLobby(user.Id, gameLobby.GameCode);
        }
    }
}
