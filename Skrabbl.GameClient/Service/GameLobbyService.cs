using System.Collections.Generic;
using System.Threading.Tasks;
using Skrabbl.GameClient.Https;
using Skrabbl.Model.Dto;

namespace Skrabbl.GameClient.Service
{
    public static class GameLobbyService
    {
        public static async Task<bool> CreateGameLobby(List<GameSettingDto> gameSettings)
        {
            var response = await HttpHelper.Post<string, List<GameSettingDto>>("gamelobby/", gameSettings);

            return response.Response.IsSuccessStatusCode;
        }
    }
}