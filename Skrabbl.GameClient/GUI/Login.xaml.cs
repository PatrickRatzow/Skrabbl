using System;
using System.Threading.Tasks;
using System.Windows;
using Skrabbl.GameClient.Service;

namespace Skrabbl.GameClient.GUI
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            LoginFromCache();
        }

        public async Task<bool> LoginFromCache()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.RefreshToken)) return false;
            if (Properties.Settings.Default.RefreshExpiresAt <= DateTime.UtcNow) return false;

            var refreshed = await UserService.RefreshToken(Properties.Settings.Default.RefreshToken);
            if (refreshed)
            {
                GoToMainWindow();
            }

            return refreshed;
        }

        public void UsernameTxtFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.Opacity = 1;
            }
        }

        public void PasswordTxtFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.Opacity = 1;
            }
        }

        private void GoToMainWindow()
        {
            MainWindow gameWindow = new MainWindow(this);
            gameWindow.Show();
            Hide();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Setup
            txtError.Text = "";

            var loggedIn =
                await UserService.Login(txtUsername.Text, txtPassword.Text, checkBoxRememberMe.IsChecked!.Value);
            if (loggedIn)
            {
                GoToMainWindow();
            }
            else
            {
                txtError.Text = "You were not able to login with that username/password combination";
            }
        }
    }
}