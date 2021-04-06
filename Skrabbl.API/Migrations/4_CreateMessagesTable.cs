using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Migrations
{
    [Migration(4)]
    public class CreateMessagesTable: Migration
    {


        public override void Down()
        {
            Delete.Table("Message");
        }

        public override void Up()
        {
            Create.Table("ChatMessage")
                .WithColumn("MessageId").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Message").AsString(255).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable();

            Create.ForeignKey("fk_ChatMessage_GameId_Game_GameLobbyId_")
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("GameLobbyId");

            Create.ForeignKey("fk_ChatMessage_UserId_User_Id")
                .FromTable("ChatMessage").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");
        }
    }
}

