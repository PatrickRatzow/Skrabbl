using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(11)]
    public class AddEndTimeAndAnswerCount : Migration
    {
        public override void Up()
        {
            Alter.Table("Turn")
                .AddColumn("EndTime").AsDateTime()
                .AddColumn("AnswerCount").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("EndTime").FromTable("Turn");
            Delete.Column("AnswerCount").FromTable("Turn");
        }
    }
}