using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(4)]
    public class CreateGameTable : Migration
    {
        public override void Up()
        {
            Create.Table("Game")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("ActiveRound").AsInt32().NotNullable()
                .WithColumn("GameLobbyCode").AsFixedLengthString(4).Nullable();

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("GameLobbyCode")
                .ToTable("GameLobby").PrimaryColumn("Code")
                .OnDelete(System.Data.Rule.SetNull);
        }

        public override void Down()
        {
            Delete.Table("Game");
        }
    }
}