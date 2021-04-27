using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(12)]
    public class AddGameLobbyForeignKeyCascade : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_GameLobby_LobbyOwnerId_Users_Id")
                .OnTable("GameLobby");

            Create.ForeignKey()
                .FromTable("GameLobby").ForeignColumn("LobbyOwnerId")
                .ToTable("Users").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Delete.ForeignKey("FK_Game_GameLobbyId_GameLobby_GameCode")
                .OnTable("Game");

            Create.ForeignKey()
                .FromTable("Game").ForeignColumn("GameLobbyId")
                .ToTable("GameLobby").PrimaryColumn("GameCode")
                .OnDelete(System.Data.Rule.Cascade);

            Delete.ForeignKey("FK_ChatMessage_GameId_Game_Id")
                .OnTable("ChatMessage");

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);


            Delete.ForeignKey("FK_Round_GameId_Game_Id")
                .OnTable("Round");

            Delete.ForeignKey("FK_Turn_RoundId_Round_Id")
                .OnTable("Turn");

            Create.ForeignKey()
                .FromTable("Round").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.ForeignKey()
                .FromTable("Turn").ForeignColumn("RoundId")
                .ToTable("Round").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
        }
    }
}