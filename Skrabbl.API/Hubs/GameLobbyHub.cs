using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
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
        public static ConcurrentDictionary<string, int> LobbyCodeToUserMap = new ConcurrentDictionary<string, int>();

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

            await Clients.Others.SendSettingChanged(key, value);
        }

        [Authorize(Policy = "HasBoughtGame")]
        public async Task AssumeControlOfLobby(string gameLobbyCode)
        {
            var lobbyOwnerId =
                int.Parse(Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            if (LobbyCodeToUserMap.ContainsKey(gameLobbyCode)) return;

            var gameLobby = await _gameLobbyService.GetGameLobbyById(gameLobbyCode);
            if (gameLobby?.LobbyOwnerId != lobbyOwnerId) return;

            if (gameLobby?.GameCode != gameLobbyCode) return;

            LobbyCodeToUserMap.TryAdd(gameLobby.GameCode, gameLobby.LobbyOwnerId);

            await Clients.Caller.ConfirmControlTakeOver();
        }
    }
}