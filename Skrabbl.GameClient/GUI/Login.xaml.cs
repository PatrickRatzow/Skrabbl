using Newtonsoft.Json;
using RestSharp;
using Skrabbl.Model;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
        //Issues
        //  - Empty settings = fail
        //  - 
        private LoginResponseDto _tokens;
        private string _portOfTheDay = "50916"; //This port number changes!

        public Login()
        {
            InitializeComponent();
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.RefreshToken != null && Properties.Settings.Default.RefreshToken != String.Empty)
            {
                //check if saved refresh token is still valid otherwise they will have to log in manually
                if (Properties.Settings.Default.RefreshExpiresAt > DateTime.Now)
                {
                    IRestResponse response_POST;
                    RestClient rest_client = new RestClient();
                    
                    string serviceURI = "http://localhost:" + _portOfTheDay + "/api/user/refresh";
                    rest_client.BaseUrl = new Uri(serviceURI);
                    RestRequest request_POST = new RestRequest(serviceURI, Method.POST);
                    RefreshDto refreshToken = new RefreshDto { Token = Properties.Settings.Default.RefreshToken};
                    request_POST.AddJsonBody(refreshToken);
                    response_POST = rest_client.Execute(request_POST);
                    _tokens = JsonConvert.DeserializeObject<LoginResponseDto>(response_POST.Content);
                    SaveTokens(_tokens);

                    if(response_POST.StatusCode == HttpStatusCode.OK)
                    {
                        OpenGameWindow(_tokens);
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //Setup
            txtError.Text = "";
            IRestResponse response_POST;
            RestClient rest_client = new RestClient();
            string ServiceURI = "http://localhost:" + _portOfTheDay + "/api/user/login";
            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_POST = new RestRequest(ServiceURI, Method.POST);
            //Create request body
            LoginDto loginData = new LoginDto { Username = txtUsername.Text, Password = txtPassword.Text };
            request_POST.AddJsonBody(loginData);
            //Execute
            response_POST = rest_client.Execute(request_POST);
            _tokens = JsonConvert.DeserializeObject<LoginResponseDto>(response_POST.Content);

            if (response_POST.StatusCode == HttpStatusCode.OK)
            {
                if (checkBoxRememberMe.IsChecked.Value)
                    SaveTokens(_tokens);
                else
                    RemoveTokenValues();

                OpenGameWindow(_tokens);
            }
            else if (response_POST.StatusCode == HttpStatusCode.Unauthorized)
            {
                txtError.Text = "Non-valid input";
            }

        }

        private void SaveTokens(LoginResponseDto tokens)
        {
            Properties.Settings.Default.JWT = tokens.Jwt.Token;
            Properties.Settings.Default.JWTExpire = tokens.Jwt.ExpiresAt;

            Properties.Settings.Default.RefreshToken = tokens.RefreshToken.Token;
            Properties.Settings.Default.RefreshExpiresAt = tokens.RefreshToken.ExpiresAt;

            Properties.Settings.Default.UserId = tokens.UserId;

            Properties.Settings.Default.Save();
        }

        public void RemoveTokenValues()
        {
            Properties.Settings.Default.JWT = String.Empty;
            Properties.Settings.Default.JWTExpire = DateTime.Now;

            Properties.Settings.Default.RefreshToken = String.Empty;
            Properties.Settings.Default.RefreshExpiresAt = DateTime.Now;

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

        private void OpenGameWindow(LoginResponseDto tokens)
        {
            LoginResponseDto resp = BuildLoginResponseFromSettings(); //Problemer hvis man ikke gemmer kode
            MainWindow gameWindow = new MainWindow(tokens, this);
            gameWindow.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
