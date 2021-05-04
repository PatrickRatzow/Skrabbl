using System;
using System.Net;
using System.Threading.Tasks;
using Skrabbl.GameClient.Https;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Service
{
    public static class UserService
    {
        public static async Task<bool> Login(string username, string password, bool rememberMe)
        {
            var data = new LoginDto
            {
                Username = username,
                Password = password,
                LobbyCreationClient = true
            };
            var response = await Task.Run(() => HttpHelper.Post<LoginResponseDto, LoginDto>("user/login", data));
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            var result = ModelMapper.Mapper.Map<Tokens>(response.Result);
            DataContainer.Tokens = result;

            if (rememberMe)
            {
                SaveTokens(DataContainer.Tokens);
            }
            else
            {
                RemoveTokenValues();
            }

            return true;
        }

        public static async Task<bool> RefreshToken(string refreshToken)
        {
            var data = new RefreshDto()
            {
                Token = refreshToken
            };
            var response = await Task.Run(() => HttpHelper.Post<LoginResponseDto, RefreshDto>("user/refresh", data));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                RemoveTokenValues();

                return false;
            }

            var result = ModelMapper.Mapper.Map<Tokens>(response.Result);
            DataContainer.Tokens = result;

            SaveTokens(DataContainer.Tokens);

            return true;
        }

        private static void SaveTokens(Tokens tokens)
        {
            Properties.Settings.Default.JWT = tokens.Jwt.Token;
            Properties.Settings.Default.JWTExpire = tokens.Jwt.ExpiresAt;

            Properties.Settings.Default.RefreshToken = tokens.RefreshToken.Token;
            Properties.Settings.Default.RefreshExpiresAt = tokens.RefreshToken.ExpiresAt;

            Properties.Settings.Default.Save();
        }

        public static void RemoveTokenValues()
        {
            Properties.Settings.Default.JWT = String.Empty;
            Properties.Settings.Default.JWTExpire = DateTime.UtcNow;

            Properties.Settings.Default.RefreshToken = String.Empty;
            Properties.Settings.Default.RefreshExpiresAt = DateTime.UtcNow;

            Properties.Settings.Default.Save();
        }
    }
}