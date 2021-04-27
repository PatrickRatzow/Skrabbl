using FluentMigrator;

namespace Skrabbl.DataAccess.Migrations
{
    [Migration(5)]
    public class CreateMessagesTable : Migration
    {
        public override void Up()
        {
            Create.Table("ChatMessage")
                .WithColumn("MessageId").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Message").AsString(255).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("GameId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable();

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("GameId")
                .ToTable("Game").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("ChatMessage").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("ChatMessage");
        }
    }
}