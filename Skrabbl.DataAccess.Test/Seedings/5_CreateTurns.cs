using System;
using System.Linq;
using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(5)]
    public class CreateTurns : Seed
    {
        private async Task CreateTurn(Round round, User user, string word)
        {
            var id = await QuerySingle<int>(@"
                INSERT INTO Turn(RoundId, UserId, Word)
                VALUES (@RoundId, @UserId, @Word);
                SELECT CAST(SCOPE_IDENTITY() AS int);
            ", new
            {
                RoundId = round.Id,
                UserId = user.Id,
                Word = word
            });

            var turn = new Turn
            {
                Id = id,
                Word = word
            };

            round.Turns.Add(turn);

            // Only on first round
            if (round.Turns.Count != 1) return;

            // Don't care about the inaccuracy
            round.Turns.First().StartTime = DateTime.Now;

            await Execute(@"
                UPDATE Round
                SET ActiveTurnId = @TurnId
                WHERE Id = @Id
            ", new
            {
                TurnId = turn.Id,
                round.Id
            });
        }

        public override async Task Up()
        {
            // Contains 5 rounds
            var patrickRounds = TestData.Games.PatrickGame.Rounds.ToArray();

            await CreateTurn(patrickRounds[0], TestData.Users.Patrick, "Cake");
            await CreateTurn(patrickRounds[0], TestData.Users.Simon, "Kage");
            await CreateTurn(patrickRounds[1], TestData.Users.Patrick, "Kage");
            await CreateTurn(patrickRounds[1], TestData.Users.Simon, "Cake");

            var xd = TestData.Users.Patrick;
        }

        public override async Task Down()
        {
            await Execute("UPDATE Round Set ActiveTurnId = NULL");
            await Execute("DELETE FROM Turn");
        }
    }
}