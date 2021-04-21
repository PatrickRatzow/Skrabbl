using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(18)]
    public class LinkChatMessagesToTurnInsteadOfGame : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_ChatMessage_GameId_Game_Id").OnTable("ChatMessage");
            Delete.Column("GameId").FromTable("ChatMessage");

            Alter.Table("ChatMessage")
                .AddColumn("TurnId").AsInt32().NotNullable().ForeignKey("Turn", "Id");
        }

        public override void Down()
        {
            Delete.Column("TurnId").FromTable("ChatMessage");

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");
        }
    }
}