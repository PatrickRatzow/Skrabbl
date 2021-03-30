using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(2)]
    public class CreateLoginTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Password").AsString(64).NotNullable()
                .AddColumn("Salt").AsString(16).NotNullable();
        }

        public override void Down()
        {
            Delete.Column("Password").FromTable("Users");
            Delete.Column("Salt").FromTable("Users");
        }
    }
}