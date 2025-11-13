using FluentMigrator;

namespace MeetingReservation.Infrastructure.Migrations.Versions
{
    [Migration(1, "Create table users and refreshTokens")]
    public class Version000001 : VersionBase
    {
        public override void Up()
        {
            CreateTable("Users")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Password").AsString(2000).NotNullable()
                .WithColumn("UserIdentifier").AsGuid().NotNullable()
                .WithColumn("Role").AsInt32().NotNullable()
                .WithColumn("CompanyId").AsInt64().NotNullable();

            CreateTable("RefreshTokens")
                .WithColumn("Value").AsString().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_RefreshTokens_User_Id", "Users", "Id");
        }
    }
}
