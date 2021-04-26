using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(14)]
    public class AddGameActiveRoundForeignKey : Migration
    {
        public override void Up()
        {
            Rename.Column("ActiveRound")
                .OnTable("Game")
                .To("ActiveRoundId");

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("ActiveRoundId")
                .ToTable("Round").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("Game").ForeignColumn("ActiveRound")
                .ToTable("Round").PrimaryColumn("Id");

            Rename.Column("ActiveRoundId")
                .OnTable("Game")
                .To("ActiveRound");
        }
    }
}