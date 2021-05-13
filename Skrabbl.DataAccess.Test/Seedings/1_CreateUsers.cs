using System.Threading.Tasks;
using Skrabbl.DataAccess.Test.Util;
using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test
{
    [Seed(1)]
    public class CreateUsers : Seed
    {
        private async Task CreateUser(User user)
        {
            var id = await QuerySingle<int>(@"
                INSERT INTO [User](Username, Email, Password, Salt, LobbyCode)
                VALUES (@Username, @Email, @Password, @Salt, NULL);
                SELECT CAST(SCOPE_IDENTITY() AS int)
            ", new
            {
                user.Username,
                user.Email,
                user.Password,
                user.Salt
            });

            user.Id = id;
        }

        public override async Task Up()
        {
            await CreateUser(TestData.Users.Patrick);
            await CreateUser(TestData.Users.Nikolaj);
            await CreateUser(TestData.Users.Floris);
            await CreateUser(TestData.Users.Simon);
        }

        public override Task Down()
        {
            return Execute("DELETE FROM [User]");
        }
    }
}