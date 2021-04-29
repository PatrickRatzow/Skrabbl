using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(23)]
    public class AddHasBoughtGameValueToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("HasBoughtGame").AsBoolean().NotNullable().WithDefaultValue(false);
        }

        public override void Down()
        {
            Delete.Column("HasBoughtGame").FromTable("Users");
        }
    }
}
