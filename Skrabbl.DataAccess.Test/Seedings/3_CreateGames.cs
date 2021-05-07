using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(3)]
    public class CreateGames : Seed
    {
        private async Task CreateGame(Game game, GameLobby gameLobby)
        {
            var id = await QuerySingle<int>(@"
                INSERT INTO Game(ActiveRoundId)
                DEFAULT VALUES;
                SELECT CAST(SCOPE_IDENTITY() AS int)
            ");

            game.Id = id;
            gameLobby.GameId = id;

            // Update the game lobby, so it has a matching game in db
            await Execute(@"
                UPDATE GameLobby
                SET GameId = @GameId
                WHERE Code = @GameCode
            ", new
            {
                gameLobby.GameId,
                gameLobby.GameCode
            });
        }

        public override async Task Up()
        {
            await CreateGame(TestData.Games.PatrickGame, TestData.GameLobbies.PatrickLobby);
            await CreateGame(TestData.Games.NikolajGame, TestData.GameLobbies.NikolajLobby);
        }

        public override async Task Down()
        {
            await Execute("UPDATE GameLobby SET GameId = NULL");
            await Execute("DELETE FROM Game");
        }
    }
}