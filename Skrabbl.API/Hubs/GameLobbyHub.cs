using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "HasBoughtGame")]
        public async Task GameLobbySettingChanged(string key, string value)
        {
            var lobbyOwnerId =
                int.Parse(Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var gameSetting = new GameSetting()
            {
                Setting = key,
                Value = value
            };
            await _gameLobbyService.UpdateGameSetting(gameSetting, lobbyOwnerId);

            await Clients.All.SendSettingChanged(key, value);
        }
    }
}