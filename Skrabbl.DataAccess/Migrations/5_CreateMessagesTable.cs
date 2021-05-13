using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(5)]
    public class CreateMessagesTable : Migration
    {
        public override void Up()
        {
            Create.Table("ChatMessage")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Message").AsString(255).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id")
                .OnDelete(System.Data.Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("ChatMessage");
        }
    }
}