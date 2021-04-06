using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Migrations
{
    [Migration(3)]
    public class CreateGameLobbyTable : Migration
    {
        public override void Down()
        {
            Delete.Table("GameLobby");
        }

        public override void Up()
        {
            Create.Table("GameLobby")
                .WithColumn("GameCode").AsFixedLengthString(4).PrimaryKey()
                .WithColumn("LobbyOwnerId").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("GameLobby").ForeignColumn("LobbyOwnerId")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}
