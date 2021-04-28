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

            // Only on first round
            if (game.Rounds.Count != 1) return;

            // Don't care about the inaccuracy

            game.ActiveRoundId = round.Id;

            await Execute(@"
                UPDATE Game
                SET ActiveRoundId = @RoundId
                WHERE Id = @Id
            ", new
            {
                RoundId = round.Id,
                game.Id
            });
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

        public override async Task Down()
        {
            await Execute("UPDATE Game SET ActiveRoundId = NULL");
            await Execute("DELETE FROM Round");
        }
    }
}