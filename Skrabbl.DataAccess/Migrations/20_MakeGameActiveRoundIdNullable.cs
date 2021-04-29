using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(20)]
    public class MakeGameActiveRoundIdNullable : Migration
    {
        public override void Up()
        {
            Alter.Table("Game")
                .AlterColumn("ActiveRoundId").AsInt32().Nullable();
        }

        public override void Down()
        {
            Alter.Table("Game")
                .AlterColumn("ActiveRoundId").AsInt32().NotNullable();
        }
    }
}