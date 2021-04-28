using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(21)]
    public class MakeTurnStartedAtDateTime : Migration
    {
        public override void Up()
        {
            Delete.DefaultConstraint().OnTable("Turn")
                .OnColumn("StartedAt");

            Alter.Table("Turn")
                .AlterColumn("StartedAt").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Alter.Table("Turn")
                .AlterColumn("StartedAt").AsDateTimeOffset().NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);
        }
    }
}