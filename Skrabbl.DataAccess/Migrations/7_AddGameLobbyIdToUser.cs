using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(7)]
    public class AddGameLobbyIdToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("GameLobbyId").AsFixedLengthString(4).Nullable();

            Create.ForeignKey()
                .FromTable("Users").ForeignColumn("GameLobbyId")
                .ToTable("GameLobby").PrimaryColumn("GameCode");
        }

        public override void Down()
        {
            Delete.Column("GameLobbyId").FromTable("Users");
        }
    }
}