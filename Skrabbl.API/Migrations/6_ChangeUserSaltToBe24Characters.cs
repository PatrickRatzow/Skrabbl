using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(6)]
    public class ChangeUserSaltToBe24Character : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AlterColumn("Salt").AsFixedLengthString(24).NotNullable();
        }

        public override void Down()
        {
            Alter.Table("Users")
                .AlterColumn("Salt").AsFixedLengthString(16).NotNullable();
        }
    }
}