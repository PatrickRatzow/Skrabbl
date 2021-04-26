using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(15)]
    public class DeleteUnneededColumnsFromTurnTable : Migration
    {
        public override void Up()
        {
            Delete.Column("EndTime").FromTable("Turn");
            Delete.Column("AnswerCount").FromTable("Turn");
        }

        public override void Down()
        {
            Alter.Table("Turn")
                .AddColumn("EndTime").AsDateTime()
                .AddColumn("AnswerCount").AsInt32().NotNullable();
        }
    }
}