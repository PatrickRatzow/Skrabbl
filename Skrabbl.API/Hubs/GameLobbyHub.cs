using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Skrabbl.Model;

namespace Skrabbl.API.Hubs
{
    public partial class GameHub : Hub<IGameHub>, IGameClient
    {
        private ConcurrentDictionary<string, string> Lobbies = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<string, string> Owners = new ConcurrentDictionary<string, string>();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            /*
            var lobbyId = Owners[Context.ConnectionId];
            if (lobbyId != null)
            {
                Lobbies.TryRemove(lobbyId, out var ownerConnection);
                Lobbies.TryRemove(Context.ConnectionId, out var lobbyIdOut);

                await Task.WhenAll(
                    _gameLobbyService.RemoveGameLobby(lobbyId),
                    Clients.All.GameLobbyDisconnected(lobbyId)
                );
            }
            */
            await base.OnDisconnectedAsync(exception);
        }

        public async Task CreateLobby(string lobbyId)
        {
            // TODO: Replace later, hardcoded for now
            var userId = 25;
            var xd = Context.User.Identity;

            if (Lobbies.ContainsKey(lobbyId) || Owners.ContainsKey(Context.ConnectionId))
                return;

            var gameLobby = _gameLobbyService.GetGameLobbyById(lobbyId);
            var user = _userService.GetUser(userId);

            await Task.WhenAll(gameLobby, user);

            if (gameLobby.Result != null || user.Result.GameLobbyId != null)
                return;

            Lobbies.TryAdd(lobbyId, Context.ConnectionId);
            Owners.TryAdd(Context.ConnectionId, lobbyId);
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);

            var newLobby = await _gameLobbyService.AddGameLobby(userId);
            await _userService.AddToLobby(userId, newLobby.GameCode);
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