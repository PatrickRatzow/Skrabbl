using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(17)]
    public class AddActiveTurnToRoundTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Round")
                .AddColumn("ActiveTurnId").AsInt32().Nullable().ForeignKey("Turn", "Id");
        }

        public override void Down()
        {
            Delete.Column("ActiveTurnId").FromTable("Round");
        }
    }
}