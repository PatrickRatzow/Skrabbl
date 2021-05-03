using RestSharp;
using Skrabbl.Model.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Skrabbl.GameClient.Https
{
    class HttpRequest
    {
        private string baseUrl = "http://localhost";
        private string port = "50916";
        private string postGameLobbyUrl = "api/gamelobby/create/";
        private string putGameLobbyUrl = "api/gamelobby/update/";

        public void PostGameLobby(int userId, List<GameSettingDto> gameSettings)
        {
            //Setup
            IRestResponse response_POST;
            RestClient rest_client = new RestClient();

            string ServiceURI = baseUrl + ":" + port + "/" + postGameLobbyUrl + userId;

            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_POST = new RestRequest(ServiceURI, Method.POST);

            request_POST.AddJsonBody(gameSettings);

            response_POST = rest_client.Execute(request_POST);


            HttpStatusCode statusCode = response_POST.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {

            }
        }
        public void PutGameLobby(int userId, List<GameSettingDto> gameSettings)
        {
            //Setup
            IRestResponse response_PUT;
            RestClient rest_client = new RestClient();

            string ServiceURI = baseUrl + ":" + port + "/" + putGameLobbyUrl + userId;

            rest_client.BaseUrl = new Uri(ServiceURI);
            RestRequest request_PUT = new RestRequest(ServiceURI, Method.PUT);

            request_PUT.AddJsonBody(gameSettings);

            response_PUT = rest_client.Execute(request_PUT);


            HttpStatusCode statusCode = response_PUT.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {

            }
        }
    }
}
