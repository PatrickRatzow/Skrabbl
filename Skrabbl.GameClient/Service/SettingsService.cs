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

namespace Skrabbl.GameClient.Service
{
    public class SettingsService
    {
        static readonly string gameLoginUrl = "https://localhost:5001/api/";
        Dictionary<string, GameSettingDto> gameSettings = new Dictionary<string, GameSettingDto>();
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
        public async Task CreateLobbyId(int userId)
        {
            await Task.Run(() =>
            {
                Post("https://localhost", "5001", $"api/gamelobby/create/{userId}", gameSettings.Values.ToList());
            });
            // return await _httpClient.PostAsync(gameURI, null);

        }
        public Task UpdateLobbyById(int userId)
        {
            return Task.Run(() =>
            {
                Put("https://localhost", "5001", $"api/gamelobby/update/{userId}", gameSettings.Values.ToList());
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
            //Post("https://localhost", "5001", $"api/gamelobby/update/{userId}", gameSettings);
            //await connection.InvokeAsync("AddGameSettings", 6, setting, value);
        }

        public async Task<HttpResponseMessage> CreateLobbyIdd(int userId) 
        {
            var gameURI = gameLoginUrl + $"gamelobby/create/{userId}";
            return await _httpClient.PostAsync(gameURI, null);
            
        }
        private void Post(string baseUrl, string port, string url, List<GameSettingDto> gameSettings)
        {
            //Setup

            IRestResponse response_POST;
            RestClient rest_client = new RestClient();

            string ServiceURI = baseUrl + ":" + port + "/" + url;


            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_POST = new RestRequest(ServiceURI, Method.POST);

            request_POST.AddJsonBody(gameSettings);

            response_POST = rest_client.Execute(request_POST);


            HttpStatusCode statusCode = response_POST.StatusCode;
            int integerStatus = (int)statusCode;

            if (integerStatus == 200)
            {

            }
        }
        private void Put(string baseUrl, string port, string url, List<GameSettingDto> gameSettings)
        {
            //Setup

            IRestResponse response_PUT;
            RestClient rest_client = new RestClient();

            string ServiceURI = baseUrl + ":" + port + "/" + url;


            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_PUT = new RestRequest(ServiceURI, Method.PUT);

            request_PUT.AddJsonBody(gameSettings);

            response_PUT = rest_client.Execute(request_PUT);


            HttpStatusCode statusCode = response_PUT.StatusCode;
            int integerStatus = (int)statusCode;

            if (integerStatus == 200)
            {

            }
        }

    }
}
