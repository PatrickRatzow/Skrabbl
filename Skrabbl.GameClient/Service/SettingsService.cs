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
        static readonly string gameLoginUrl = "http://localhost:5001/api/user/login";
        HubConnection connection;
        readonly HttpClient _httpClient;

        public SettingsService()
        {
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:50916/ws/game")
            .Build();

            _httpClient = new HttpClient();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
        public async Task HardCodedLogin()
        {
            string username = "simon";
            string password = "asdfghjk";

            var uri = new Uri(gameLoginUrl);
            
            //Also check if userId exists
                try
                {
                    LoginDto loginInformation = new LoginDto()
                    {
                        Username = username,
                        Password = password

                    };
                    var json = JsonConvert.SerializeObject(loginInformation);

                    HttpContent userInfo = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(uri, userInfo);
                    string responseContent = await response.Content.ReadAsStringAsync();

                Debug.WriteLine("You are logged in");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
        }
    }
}
