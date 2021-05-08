using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(2)]
    public class CreateGameLobbies : Seed
    {
        private Task CreateGameLobby(GameLobby gameLobby, User user)
        {
            return Execute(@"
                    INSERT INTO GameLobby(Code, LobbyOwnerId, GameId)
                    VALUES (@Code, @LobbyOwnerId, NULL)
                ", new
            {
                gameLobby.Code,
                LobbyOwnerId = user.Id
            });
        }

        public override async Task Up()
        {
            await CreateGameLobby(TestData.GameLobbies.PatrickLobby, TestData.Users.Patrick);
            await CreateGameLobby(TestData.GameLobbies.NikolajLobby, TestData.Users.Nikolaj);
        }

        public override async Task Down()
        {
            await Execute("DELETE FROM GameSetting");
            await Execute("DELETE FROM GameLobby");
        }
    }
}
