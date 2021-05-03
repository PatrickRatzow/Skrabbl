using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(22)]
    public class GameSetting : Migration

    {
        public override void Up()
        {
            Create.Table("GameSetting")
                .WithColumn("GameCode").AsFixedLengthString(4)
                .WithColumn("Setting").AsString(255).NotNullable()
                .WithColumn("Value").AsString(int.MaxValue).NotNullable();

            Create.ForeignKey()
              .FromTable("GameSetting").ForeignColumn("GameCode")
              .ToTable("GameLobby").PrimaryColumn("GameCode");

            Create.PrimaryKey()
                .OnTable("GameSetting")
                .Columns("GameCode", "Setting");
        }

        public override void Down()
        {
            Delete.Table("GameSetting");
        }
    }
}
