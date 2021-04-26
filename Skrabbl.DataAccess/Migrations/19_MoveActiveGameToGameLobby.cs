using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(19)]
    public class MoveActiveGameToGameLobby : Migration
    {
        public override void Up()
        {
            Alter.Table("GameLobby")
                .AddColumn("GameId").AsInt32().Nullable().ForeignKey("Game", "Id").Indexed();

            Delete.ForeignKey("FK_Game_GameLobbyId_GameLobby_GameCode").OnTable("Game");
            Delete.Column("GameLobbyId").FromTable("Game");
        }

        public override void Down()
        {
            Delete.Column("GameId").FromTable("GameLobby");

            Alter.Table("GameLobby")
                .AddColumn("GameLobbyId").AsFixedLengthString(4);

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("GameLobbyId")
                .ToTable("GameLobby").PrimaryColumn("GameCode");
        }
    }
}