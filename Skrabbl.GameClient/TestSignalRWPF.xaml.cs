using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.AspNetCore.SignalR.Client;

namespace Skrabbl.GameClient
{
    /// <summary>
    /// Interaction logic for TestSignalRWPF.xaml
    /// </summary>
    public partial class TestSignalRWPF : Window
    {
        static readonly string gameLobbyUrl = "http://localhost:50916/api/gamelobby/";
        HubConnection connection;
        readonly HttpClient _httpClient;

        public TestSignalRWPF()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:50916/ChatHub")
            .Build();

            _httpClient = new HttpClient();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            connection.On<string, string>("ReceiveMessage", (user, msg) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMsg = $"{user}: {msg}";
                    listMsg.Items.Add(newMsg);

                });
            });

            try
            {
                await connection.StartAsync();
                listMsg.Items.Add("Connection started!");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                listMsg.Items.Add("Connection could not start: " + ex.Message);
            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", firstNameText.Text, msgText.Text);
            }
            catch (Exception ex)
            {
                listMsg.Items.Add(ex.Message);
            }
        }

        private async void startGame_Click(object sender, RoutedEventArgs e)
        {
            //User Id instead of 25.
            await startGame(31);
        }

        //"/api/gamelobby/{userId}"
        private async Task startGame(int userId)
        {
            //Create a new game
            var uri = new Uri(gameLobbyUrl + userId.ToString());
            var existingGameLobby = await GetGameLobby(userId);
            //Also check if userId exists
            if (existingGameLobby.StatusCode.Equals(HttpStatusCode.NotFound))
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsync(uri, null);
                    string resultingIdString = await response.Content.ReadAsStringAsync();
                    resultLbl.Text = uri + " Lobby was created!";
                } catch
                {
                    resultLbl.Text = "Lobby was not created! - BOHO";
                }
            }
            else
            {
                resultLbl.Text = $"User with id {userId} already have a lobby!";
            }

        }

        // /api/gamelobby/user/{id}
        private async Task<HttpResponseMessage> GetGameLobby(int userId)
        {
            var uri = new Uri(gameLobbyUrl + $"user/{userId}");

            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            return response;
        }
    }
}
