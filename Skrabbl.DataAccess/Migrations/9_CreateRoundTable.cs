using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(9)]
    public class CreateRoundTable : Migration
    {
        public override void Up()
        {
            Create.Table("Round")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoundNumber").AsInt32().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable();

            Create.UniqueConstraint()
                .OnTable("Round").Columns("RoundNumber", "GameId");

            Create.ForeignKey()
                .FromTable("Round").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Round");
        }
    }
}