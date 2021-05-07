using FluentMigrator;
using System;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(19)]
    public class MoveActiveGameToGameLobby : Migration
    {
        public override void Up()
        {
            Alter.Table("GameLobby")
                .AddColumn("GameId").AsInt32().Nullable().ForeignKey("Game", "Id").Indexed();
        }

        public override void Down()
        {
            Delete.Index("IX_GameLobby_GameId").OnTable("GameLobby");

            Delete.ForeignKey()
                .FromTable("GameLobby").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");

           

            Delete.Column("GameId").FromTable("GameLobby");
        }

        private void Sql(string v)
        {
            throw new NotImplementedException();
        }
    }
}