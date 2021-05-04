using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Skrabbl.GameClient.Service;

namespace Skrabbl.GameClient.Helper
{
    public static class SignalR
    {
        public static readonly HubConnection Connection = new HubConnectionBuilder()
            .WithUrl($"{HttpHelper.Url}/ws/game", options => { options.AccessTokenProvider = AccessToken; })
            .WithAutomaticReconnect()
            .Build();

        private static async Task<string?> AccessToken()
        {
            if (DataContainer.Tokens == null) return null;
            if (!DataContainer.IsTokenExpired()) return DataContainer.Tokens.Jwt.Token;

            var refreshed = await UserService.RefreshToken();
            if (!refreshed) return null;

            return DataContainer.Tokens.Jwt.Token;
        }

        public static Task Connect()
        {
            if (Connection.State == HubConnectionState.Connected) return Task.CompletedTask;

            return Connection.StartAsync();
        }
    }
}