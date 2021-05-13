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
                .WithColumn("GameLobbyCode").AsFixedLengthString(4)
                .WithColumn("SettingType").AsString(255).NotNullable()
                .WithColumn("Value").AsString(int.MaxValue).NotNullable();

            Create.ForeignKey()
              .FromTable("GameSetting").ForeignColumn("GameLobbyCode")
              .ToTable("GameLobby").PrimaryColumn("Code")
              .OnDelete(System.Data.Rule.Cascade);

            Create.PrimaryKey()
                .OnTable("GameSetting")
                .Columns("GameLobbyCode", "SettingType");
        }

        public override void Down()
        {
            Delete.Table("GameSetting");
        }
    }
}
