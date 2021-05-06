using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(3)]
    public class CreateGameLobbyTable : Migration
    {
        public override void Up()
        {
            Create.Table("GameLobby")
                .WithColumn("Code").AsFixedLengthString(4).PrimaryKey()
                .WithColumn("LobbyOwnerId").AsInt32().Nullable();

            Create.ForeignKey()
                .FromTable("GameLobby").ForeignColumn("LobbyOwnerId")
                .ToTable("User").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.SetNull);
        }

        public override void Down()
        {
            Delete.Table("GameLobby");
        }
    }
}