using Newtonsoft.Json;
using Skrabbl.GameClient.Service;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Skrabbl.GameClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] players = { 2, 3, 5, 8, 13, 21 };
        int[] drawingTime = { 30, 45, 60, 75, 90, 105, 120 };
        private SettingsService _settingsService;
        private int userId;
        private string JWT;

        public MainWindow(string JWT)
        {
            InitializeComponent();
            tbCustomWords.Text = JWT;
            _settingsService = new SettingsService();
            var id = JsonConvert.DeserializeObject<LoginResponseDto>(JWT);
            userId = id.UserId;

            var gotJwt = JsonConvert.DeserializeObject<LoginResponseDto>(JWT);
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
            
            await _settingsService.CreateLobbyId(userId);
            tbCustomWords.Text = "GameLobby created :)";
           // Game newgame = new Game(players[comboPlayers.SelectedIndex], comboRounds.SelectedIndex + 1, drawingTime[comboDrawingTime.SelectedIndex]);
        }
    }
}
