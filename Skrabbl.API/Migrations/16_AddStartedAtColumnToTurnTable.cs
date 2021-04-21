using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(16)]
    public class AddStartedAtColumnToTurnTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Turn")
                .AddColumn("StartedAt").AsDateTimeOffset().NotNullable()
                .WithDefault(SystemMethods.CurrentDateTimeOffset);
        }

        public override void Down()
        {
            Delete.Column("StartedAt").FromTable("Turn");
        }
    }
}