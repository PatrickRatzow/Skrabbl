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
                .WithColumn("GameLobbyId").AsFixedLengthString(4);

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("GameLobbyId")
                .ToTable("GameLobby").PrimaryColumn("GameCode");
        }

        public override void Down()
        {
            Delete.Table("Game");
        }
    }
}