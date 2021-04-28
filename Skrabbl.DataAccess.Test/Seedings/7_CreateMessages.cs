using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(7)]
    public class CreateMessages : Seed
    {
        private Task InsertMessages(Turn turn, List<ChatMessage> messages)
        {
            // Add to the in-memory object
            turn.Messages.AddRange(messages);

            // SQL
            var bindings = new object[messages.Count * 4];
            var id = 0;
            var inserts = string.Join(", ", messages.Select(msg =>
            {
                bindings[id] = msg.Message;
                bindings[id + 1] = msg.CreatedAt;
                bindings[id + 2] = msg.User.Id;
                bindings[id + 3] = turn.Id;

                // Outputs @P1, @P2, @P3, @P4 when id is 0 at the beginning, etc.
                return $"(@P{++id}, @P{++id}, @P{++id}, @P{++id})";
            }));
            var query = $@"
                INSERT INTO ChatMessage(Message, CreatedAt, UserId, TurnId)
                VALUES {inserts}
            ";

            var args = new DynamicParameters();
            for (var i = 0; i < bindings.Length; i++) args.Add($"@P{i + 1}", bindings[i]);

            return Execute(query, args);
        }

        public override Task Up()
        {
            return InsertMessages(TestData.Games.PatrickGame.Rounds.First().Turns.First(),
                new List<ChatMessage>
                {
                    new ChatMessage
                    {
                        CreatedAt = DateTime.UtcNow.AddSeconds(-5),
                        User = TestData.Users.Patrick,
                        Game = TestData.Games.PatrickGame,
                        Message = "Kage"
                    },
                    new ChatMessage
                    {
                        CreatedAt = DateTime.UtcNow.AddSeconds(-3),
                        User = TestData.Users.Patrick,
                        Game = TestData.Games.PatrickGame,
                        Message = "Cake"
                    },
                    new ChatMessage
                    {
                        CreatedAt = DateTime.UtcNow.AddSeconds(-5),
                        User = TestData.Users.Simon,
                        Game = TestData.Games.PatrickGame,
                        Message = "Kage"
                    }
                });
        }

        public override async Task Down()
        {
            await Execute("DELETE FROM ChatMessage");
        }
    }
}