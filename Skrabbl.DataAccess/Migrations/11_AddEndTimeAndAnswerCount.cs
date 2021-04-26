using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skrabbl.API.Migrations
{
    [Migration(11)]
    public class AddEndTimeAndAnswerCount: Migration
    {
        
        public override void Up()
        {
            Alter.Table("Turn")
                .AddColumn("EndTime").AsDateTime()
                .AddColumn("AnswerCount").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("EndTime").FromTable("Turn");
            Delete.Column("AnswerCount").FromTable("Turn");
        }
    }
}

