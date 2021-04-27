using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(4)]
    public class CreateRounds : Seed
    {
        private async Task CreateRound(Game game)
        {
            var id = await QuerySingle<int>(@"
                INSERT INTO Round(RoundNumber, GameId)
                VALUES (@RoundNumber, @GameId);
                SELECT CAST(SCOPE_IDENTITY() AS int)
            ", new
            {
                RoundNumber = game.Rounds.Count + 1,
                GameId = game.Id
            });

            var round = new Round
            {
                RoundNumber = game.Rounds.Count + 1,
                GameId = game.Id,
                Id = id
            };

            game.Rounds.Add(round);
        }

        public override async Task Up()
        {
            for (var i = 0; i < 5; i++)
            {
                await CreateRound(TestData.Games.PatrickGame);
            }

            for (var i = 0; i < 3; i++)
            {
                await CreateRound(TestData.Games.NikolajGame);
            }
        }

        public override Task Down()
        {
            return Execute("DELETE FROM Round");
        }
    }
}