using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(14)]
    public class AddGameActiveRoundForeignKey : Migration
    {
        public override void Up()
        {
            Rename.Column("ActiveRound")
                .OnTable("Game")
                .To("ActiveRoundId");

            Alter.Table("Game")
                .AlterColumn("ActiveRoundId")
                .AsInt32()
                .Nullable();

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("ActiveRoundId")
                .ToTable("Round").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("Game").ForeignColumn("ActiveRoundId")
                .ToTable("Round").PrimaryColumn("Id");

            Rename.Column("ActiveRoundId")
                .OnTable("Game")
                .To("ActiveRound");

            Alter.Table("Game")
                .AlterColumn("ActiveRound")
                .AsInt32()
                .NotNullable();
        }
    }
}