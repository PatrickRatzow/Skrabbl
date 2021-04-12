using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Skrabbl.API.Services;

namespace Skrabbl.API.Hubs
{
    public class GameLobbyHub : Hub
    {
        private readonly IGameLobbyService _gameLobbyService;
        public ConcurrentDictionary<string, string> Lobbies = new ConcurrentDictionary<string, string>();
        public ConcurrentDictionary<string, string> Owners = new ConcurrentDictionary<string, string>();
        
        public GameLobbyHub(IGameLobbyService gameLobbyService)
        {
            _gameLobbyService = gameLobbyService;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var lobbyId = Owners[Context.ConnectionId];
            if (lobbyId != null)
            {
                Lobbies.TryRemove(lobbyId, out var ownerConnection);
                Lobbies.TryRemove(Context.ConnectionId, out var lobbyIdOut);
                
                await Task.WhenAll(
                    _gameLobbyService.RemoveGameLobby(lobbyId), 
                    Clients.All.SendAsync("GameLobbyDisconnected", lobbyId)
                );
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task CreateLobby(string lobbyId)
        {
            if (Lobbies.ContainsKey(lobbyId) || Owners.ContainsKey(Context.ConnectionId))
                return;
            
            var gameLobby = await _gameLobbyService.GetGameLobbyById(lobbyId);
            if (gameLobby == null)
                return;

            Lobbies.TryAdd(lobbyId, Context.ConnectionId);
            Owners.TryAdd(Context.ConnectionId, lobbyId);
        }
    }
}