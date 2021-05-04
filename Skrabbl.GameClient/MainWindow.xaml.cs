using System;
using System.Windows;
using Newtonsoft.Json;
using RestSharp;
using Skrabbl.GameClient.GUI;
using Skrabbl.GameClient.Https;
using Skrabbl.GameClient.Service;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] players = {2, 3, 5, 8, 13, 21};
        int[] drawingTime = {30, 45, 60, 75, 90, 105, 120};
        private Login _loginWindow;
        private SettingsService _settingsService;
        private int userId;

        public MainWindow(Login loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _settingsService = new SettingsService();
            // userId = JWT.UserId;
            //var id = JsonConvert.DeserializeObject<LoginResponseDto>(JWT);
            //userId = id.UserId;

            //var gotJwt = JsonConvert.DeserializeObject<LoginResponseDto>(JWT);
            //JWT = gotJwt.JWT

            foreach (int i in players)
            {
                comboPlayers.Items.Add(i + " Players");
                comboPlayers.SelectedIndex = 3;
            }

            for (int i = 1; i < 10; i++)
            {
                comboRounds.Items.Add(i + " Rounds");
                comboRounds.SelectedIndex = 3;
            }

            foreach (int i in drawingTime)
            {
                comboDrawingTime.Items.Add(i + " Seconds");
                comboDrawingTime.SelectedIndex = 3;
            }
        }

        public async void StartGame(object sender, RoutedEventArgs e)
        {
            //Check If user does not already have a lobby
            await _settingsService.CreateLobbyId(userId);
            tbCustomWords.Text = "GameLobby created :)";
        }

        private async void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            //Make logout request to API so it deletes the tokens
            //IRestResponse response_POST;
            //RestClient rest_client = new RestClient();

            //string serviceURI = "http://localhost:" + _portOfTheDay + "/api/user/logout";
            //rest_client.BaseUrl = new Uri(serviceURI);
            //RestRequest request_POST = new RestRequest(serviceURI, Method.POST);

            //request_POST.AddJsonBody(refreshToken);

            //response_POST = rest_client.Execute(request_POST);
            //Tokens = JsonConvert.DeserializeObject<LoginResponseDto>(response_POST.Content);

            RefreshDto refreshToken = new RefreshDto { Token = Properties.Settings.Default.RefreshToken };

            var response = await HttpHelper.Post<LoginResponseDto, RefreshDto>("user/logout", refreshToken);
            DataContainer.Tokens = null;

            //Open up the login window
            _loginWindow.Visibility = Visibility.Visible;
            UserService.RemoveTokenValues();
            this.Visibility = Visibility.Collapsed;
        }

        public void MaxPlayersChanged(object sender, RoutedEventArgs e)
        {
            _settingsService.SettingsUpdateOnChange("MaxPlayers", players[comboPlayers.SelectedIndex].ToString(),
                userId);
        }

        public void NoOfRoundsChanged(object sender, RoutedEventArgs e)
        {
            int noOfRounds = comboRounds.SelectedIndex + 1;
            _settingsService.SettingsUpdateOnChange("NoOfRounds", noOfRounds.ToString(), userId);
        }

        public void DrawingTimeChanged(object sender, RoutedEventArgs e)
        {
            _settingsService.SettingsUpdateOnChange("TurnTime", drawingTime[comboDrawingTime.SelectedIndex].ToString(),
                userId);
        }
    }
}
