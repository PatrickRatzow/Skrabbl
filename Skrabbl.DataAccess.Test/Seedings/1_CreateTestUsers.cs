using System.Threading.Tasks;

namespace Skrabbl.DataAccess.Test.Seedings
{
    [Seed(1)]
    public class CreateTestUsers : Seed
    {
        public override Task Up()
        {
            return Execute(@"
                INSERT INTO Users(Username, Email, Password, Salt, GameLobbyId)
                VALUES ('Patrick', 'patrick@email.dk', @Password, @Salt, NULL)
            ", new
            {
                Password = "pLNZSux4l2ar1z6PKh4tiBSZ25OSaim5R1bmXuD+aS8=",
                Salt = "mvV8K4PoKh41psKjxAWTGQ=="
            });
        }

        public override Task Down()
        {
            return Execute("DELETE FROM Users");
        }
    }
}