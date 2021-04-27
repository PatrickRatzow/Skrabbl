using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test
{
    [Seed(6)]
    public class CreateMessages : Seed
    {
        public override Task Up()
        {
            return Task.CompletedTask;
        }

        public override async Task Down()
        {
            await Execute("UPDATE ChatMessage SET TurnId = NULL");
            await Execute("DELETE FROM ChatMessage");
        }
    }
}