using RestSharp;
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
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
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
            string PortOfTheDay = "50916"; //This port number changes!
            string ServiceURI = "http://localhost:" + PortOfTheDay + "/api/user/login";


            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_POST = new RestRequest(ServiceURI, Method.POST);
            LoginDto loginData = new LoginDto { Username = txtUsername.Text, Password = txtPassword.Text};
                
            request_POST.AddJsonBody(loginData);

            response_POST = rest_client.Execute(request_POST);


            HttpStatusCode statusCode = response_POST.StatusCode;
            int integerStatus = (int)statusCode;

            if (integerStatus == 200)
            {
                MainWindow gameWindow = new MainWindow(response_POST.Content);
                //this will open your child window
                gameWindow.Show();
                //this will close parent window. windowOne in this case
                this.Close();
            }
            else if(integerStatus == 401)
            {
                txtError.Text = "Wrong combination of username and password"; 
            }
            
        }
    }
}
