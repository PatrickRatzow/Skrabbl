using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(1)]
    public class CreateLoginTable : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}