using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Skrabbl.GameClient.Helper;
using Skrabbl.Model;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Service
{
    public static class GameLobbyService
    {
        private static readonly Dictionary<string, GameSetting> GameSettings = new Dictionary<string, GameSetting>();

        public static async Task<bool> CreateGameLobby()
        {
            var gameSettings = ModelMapper.Mapper.Map<List<GameSettingDto>>(GameSettings.Values);
            var response = await HttpHelper.Post<string, List<GameSettingDto>>("gamelobby", gameSettings);

            if (response.Response.IsSuccessStatusCode)
            {
                DataContainer.GameLobby = new GameLobby(); // TODO: Retrieve actual object
                await SignalR.Connect();

                return true;
            }

            return false;
        }

        public static void SettingChanged(string setting, string value)
        {
            var contained = GameSettings.ContainsKey(setting);
            var gameSetting = new GameSetting()
            {
                Setting = setting,
                Value = value
            };
            GameSettings[setting] = gameSetting;

            if (DataContainer.GameLobby == null || !contained) return;

            UpdateSetting(gameSetting);
        }

        private static Task UpdateSetting(GameSetting gameSetting)
        {
            return SignalR.Connection.SendAsync("GameLobbySettingChanged", gameSetting.Setting, gameSetting.Value);
        }
    }
}