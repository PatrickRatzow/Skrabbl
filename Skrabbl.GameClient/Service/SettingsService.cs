using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RestSharp;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Skrabbl.GameClient.Https;

namespace Skrabbl.GameClient.Service
{
    public class SettingsService
    {
        static readonly string gameLoginUrl = "http://localhost:50916/api/";
        Dictionary<string, GameSettingDto> gameSettings = new Dictionary<string, GameSettingDto>();
        HubConnection connection;
        readonly HttpClient _httpClient;
        readonly HttpRequest _http;

        public SettingsService()
        {
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:50916/ws/game")
            .Build();

            _httpClient = new HttpClient();
            _http = new HttpRequest();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
        public async Task CreateLobbyId(int userId)
        {
            await _http.PostGameLobby(userId, gameSettings.Values.ToList());
            // return await _httpClient.PostAsync(gameURI, null);

        }
        public Task UpdateLobbyById(int userId)
        {
            return Task.Run(() =>
            {
                _http.PutGameLobby(userId, gameSettings.Values.ToList());
            });
            // return await _httpClient.PostAsync(gameURI, null);

        }

        public void SettingsUpdateOnChange(string setting, string value, int userId)
        {
            if (this.gameSettings.ContainsKey(setting))
            {
                this.gameSettings[setting] = new GameSettingDto
                {
                    Setting = setting,
                    Value = value
                };
                UpdateLobbyById(userId);
            }
            else
            {
                this.gameSettings[setting] = new GameSettingDto
                {
                    Setting = setting,
                    Value = value
                };

            }
            //Post("http://localhost", "50916", $"api/gamelobby/update/{userId}", gameSettings);
            //await connection.InvokeAsync("AddGameSettings", 6, setting, value);
        }

        public async Task<HttpResponseMessage> CreateLobbyIdd(int userId)
        {
            var gameURI = gameLoginUrl + $"gamelobby/create/{userId}";
            return await _httpClient.PostAsync(gameURI, null);

        }
    }
}
