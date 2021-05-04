using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.Model.Dto
{
    public class GameLobbyDto
    {
        public string GameCode { get; set; }
        public int LobbyOwnerId { get; set; }
        public List<GameSettingDto> GameSettings { get; set; }
    }
}
