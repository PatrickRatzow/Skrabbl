using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skrabbl.GameClient.Service
{
    public class SettingsService
    {
        static readonly string gameLoginUrl = "https://localhost:5001/api/";
        HubConnection connection;
        readonly HttpClient _httpClient;

        public SettingsService()
        {
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5001/ws/game")
            .Build();

            _httpClient = new HttpClient();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        public async Task<HttpResponseMessage> CreateLobbyId(int userId) 
        {
            var gameURI = gameLoginUrl + $"gamelobby/create/{userId}";
            return await _httpClient.PostAsync(gameURI, null);
            
        }
      
    }
}
