using FluentMigrator;

namespace Skrabbl.API.Migrations
{
    [Migration(8)]
    public class CreateWordList : Migration
    {
        public override void Up()
        {
            Create.Table("WordList")
                .WithColumn("Word").AsString(30).PrimaryKey().NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("WordList");
        }
    }
}