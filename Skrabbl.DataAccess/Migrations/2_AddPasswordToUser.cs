using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(2)]
    public class AddPasswordToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("Password").AsString(64).NotNullable()
                .AddColumn("Salt").AsString(16).NotNullable();
        }

        public override void Down()
        {
            Delete.Column("Password").FromTable("User");
            Delete.Column("Salt").FromTable("User");
        }
    }
}