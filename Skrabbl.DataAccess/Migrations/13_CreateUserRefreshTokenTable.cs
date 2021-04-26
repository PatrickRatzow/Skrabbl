using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(13)]
    public class CreateUserRefreshTokenTable : Migration
    {
        public override void Up()
        {
            Create.Table("UserRefreshToken")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Token").AsString(64).NotNullable().Unique()
                .WithColumn("ExpiresAt").AsDateTime().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id");
        }

        public override void Down()
        {
            Delete.Table("UserRefreshToken");
        }
    }
}