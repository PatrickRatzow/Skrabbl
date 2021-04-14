using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Migrations
{
    [Migration(9)]
    public class CreateRoundTable : Migration
    {
        public override void Up()
        {
            Create.Table("Round")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoundNumber").AsInt32().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable();

            Create.UniqueConstraint()
                .OnTable("Round").Columns("RoundNumber", "GameId");

            Create.ForeignKey()
                .FromTable("Round").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.Table("Round");
        }


    }
}
