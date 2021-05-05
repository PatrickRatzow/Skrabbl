using System.Windows;
using Skrabbl.GameClient.GUI;
using Skrabbl.GameClient.Service;

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

        public MainWindow(Login loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;

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
            await GameLobbyService.CreateGameLobby();
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

            await UserService.Logout();

            //Open up the login window
            _loginWindow.Visibility = Visibility.Visible;
            Visibility = Visibility.Collapsed;
        }

        public void MaxPlayersChanged(object sender, RoutedEventArgs e)
        {
            GameLobbyService.SettingChanged(MaxPlayers.Name, players[comboPlayers.SelectedIndex].ToString());
        }

        public void NoOfRoundsChanged(object sender, RoutedEventArgs e)
        {
            int noOfRounds = comboRounds.SelectedIndex + 1;
            GameLobbyService.SettingChanged("NoOfRounds", noOfRounds.ToString());
        }

        public void DrawingTimeChanged(object sender, RoutedEventArgs e)
        {
            GameLobbyService.SettingChanged("TurnTime", drawingTime[comboDrawingTime.SelectedIndex].ToString());
        }
    }
}