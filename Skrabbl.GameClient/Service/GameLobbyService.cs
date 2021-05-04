using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.GameClient.Https;
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

            return response.Response.IsSuccessStatusCode;
        }

        public static void SettingChanged(string setting, string value)
        {
            var gameSetting = new GameSetting()
            {
                Setting = setting,
                Value = value
            };
            GameSettings[setting] = gameSetting;

            if (DataContainer.GameLobby == null) return;

            UpdateSetting(gameSetting);
        }

        private static Task UpdateSetting(GameSetting gameSetting)
        {
            return Task.CompletedTask;
        }
    }
}