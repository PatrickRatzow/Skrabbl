using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(7)]
    public class AddGameLobbyIdToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("LobbyCode").AsFixedLengthString(4).Nullable();

            Create.ForeignKey()
                .FromTable("User").ForeignColumn("LobbyCode")
                .ToTable("GameLobby").PrimaryColumn("Code");
        }

        public override void Down()
        {
            Delete.ForeignKey()
                .FromTable("User").ForeignColumn("LobbyCode")
                .ToTable("GameLobby").PrimaryColumn("Code");

            Delete.Column("LobbyCode").FromTable("User");
        }
    }
}