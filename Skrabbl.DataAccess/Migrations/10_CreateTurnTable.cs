using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(10)]
    public class CreateTurnTable : Migration
    {
        public override void Up()
        {
            Create.Table("Turn")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoundId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("Word").AsString(255);

            Create.ForeignKey()
                .FromTable("Turn").ForeignColumn("RoundId")
                .ToTable("Round").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Turn").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Turn");
        }
    }
}