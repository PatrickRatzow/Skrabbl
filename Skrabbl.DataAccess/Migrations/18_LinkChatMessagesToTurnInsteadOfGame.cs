using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(18)]
    public class LinkChatMessagesToTurnInsteadOfGame : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");
            Delete.Column("GameId").FromTable("ChatMessage");

            Alter.Table("ChatMessage")
                .AddColumn("TurnId").AsInt32().NotNullable().ForeignKey("Turn", "Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_ChatMessage_TurnId_Turn_Id").OnTable("ChatMessage");
            Delete.Column("TurnId").FromTable("ChatMessage");


            Alter.Table("ChatMessage").AddColumn("GameId").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");
        }
    }
}