using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(6)]
    public class UsersJoinLobbies : Seed
    {
        private Task JoinLobby(User user, GameLobby gameLobby)
        {
            return Execute(@"
                UPDATE [User]
                SET LobbyCode = @LobbyCode
                WHERE Id = @UserId
            ", new
            {
                LobbyCode = gameLobby.Code,
                UserId = user.Id
            });
        }

        public override async Task Up()
        {
            await JoinLobby(TestData.Users.Patrick, TestData.GameLobbies.PatrickLobby);
            await JoinLobby(TestData.Users.Nikolaj, TestData.GameLobbies.NikolajLobby);
            await JoinLobby(TestData.Users.Simon, TestData.GameLobbies.PatrickLobby);
        }

        public override Task Down()
        {
            return Execute("UPDATE [User] SET LobbyCode = NULL");
        }
    }
}