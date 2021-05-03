using System.Collections.Generic;

namespace Skrabbl.Model
{
    public class GameLobby
    {
        public string GameCode { get; set; }
        public int LobbyOwnerId { get; set; }
        public int GameId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public List<GameSetting> GameSettings { get; set; }
    }
}