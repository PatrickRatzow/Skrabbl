using Newtonsoft.Json;
using RestSharp;
using Skrabbl.GameClient.Https;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Net;
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


namespace Skrabbl.GameClient.GUI
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            AttemptToFindLoginInCache().GetAwaiter().GetResult();
        }

        public async Task<HttpHelperResponse<LoginResponseDto>> PostRefreshToken()
        {
            RefreshDto refreshToken = new RefreshDto { Token = Properties.Settings.Default.RefreshToken };
            var response = await HttpHelper.Post<LoginResponseDto, RefreshDto>("user/refresh", refreshToken);
            var tokens = ModelMapper.Mapper.Map<Tokens>(response.Result);
            DataContainer.Tokens = tokens;

            return response;
        }

        public async Task AttemptToFindLoginInCache()
        {
            if (Properties.Settings.Default.RefreshToken != null && Properties.Settings.Default.RefreshToken != String.Empty)
            {
                //check if saved refresh token is still valid otherwise they will have to log in manually
                if (Properties.Settings.Default.RefreshExpiresAt > DateTime.UtcNow)
                {
                    var response = await PostRefreshToken();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        SaveTokens(DataContainer.Tokens);
                        OpenGameWindow(DataContainer.Tokens);
                    }
                }
            }
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

        public async Task<HttpHelperResponse<LoginResponseDto>> PostLogin(string userName, string password)
        {
            LoginDto loginData = new LoginDto { Username = userName, Password = password, LobbyCreationClient = true };

            try
            {
                var response = await HttpHelper.Post<LoginResponseDto, LoginDto>("user/login", loginData);
                var tokens = ModelMapper.Mapper.Map<Tokens>(response.Result);
                DataContainer.Tokens = tokens;

                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Setup
            txtError.Text = "";
            //IRestResponse response_POST;
            //RestClient rest_client = new RestClient();
            //string ServiceURI = "http://localhost:" + _portOfTheDay + "/api/user/login";
            //rest_client.BaseUrl = new Uri(ServiceURI);
            //RestRequest request_POST = new RestRequest(ServiceURI, Method.POST);
            ////Create request body
            //LoginDto loginData = new LoginDto { Username = txtUsername.Text, Password = txtPassword.Text, LobbyCreationClient = true };
            //request_POST.AddJsonBody(loginData);
            ////Execute
            //response_POST = rest_client.Execute(request_POST);
            //_tokens = JsonConvert.DeserializeObject<LoginResponseDto>(response_POST.Content);

            var response = PostLogin(txtUsername.Text, txtPassword.Text).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (checkBoxRememberMe.IsChecked.Value)
                    SaveTokens(DataContainer.Tokens);
                else
                    RemoveTokenValues();

                OpenGameWindow(DataContainer.Tokens);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                txtError.Text = "Non-valid input";
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                txtError.Text = "Access not granted";
            }

        }

        private void SaveTokens(Tokens tokens)
        {
            Properties.Settings.Default.JWT = tokens.Jwt.Token;
            Properties.Settings.Default.JWTExpire = tokens.Jwt.ExpiresAt;

            Properties.Settings.Default.RefreshToken = tokens.RefreshToken.Token;
            Properties.Settings.Default.RefreshExpiresAt = tokens.RefreshToken.ExpiresAt;

            Properties.Settings.Default.Save();
        }

        public void RemoveTokenValues()
        {
            Properties.Settings.Default.JWT = String.Empty;
            Properties.Settings.Default.JWTExpire = DateTime.UtcNow;

            Properties.Settings.Default.RefreshToken = String.Empty;
            Properties.Settings.Default.RefreshExpiresAt = DateTime.UtcNow;

            Properties.Settings.Default.UserId = 0;
            Properties.Settings.Default.Save();
        }

        private LoginResponseDto BuildLoginResponseFromSettings()
        {
            //Building the Token structure
            LoginResponseDto resp = new LoginResponseDto()
            {
                Jwt = new Jwt()
                {
                    Token = Properties.Settings.Default.JWT,
                    ExpiresAt = Properties.Settings.Default.JWTExpire
                },
                RefreshToken = new RefreshToken()
                {
                    Token = Properties.Settings.Default.RefreshToken,
                    ExpiresAt = Properties.Settings.Default.RefreshExpiresAt
                    //Dont know if i should get the user here
                },
                UserId = Properties.Settings.Default.UserId
            };

            return resp;
        }

        private void OpenGameWindow(Tokens tokens)
        {
            LoginResponseDto resp = BuildLoginResponseFromSettings(); //Problemer hvis man ikke gemmer kode
            MainWindow gameWindow = new MainWindow(tokens, this);
            gameWindow.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
