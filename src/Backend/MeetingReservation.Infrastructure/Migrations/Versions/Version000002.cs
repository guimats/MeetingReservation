using FluentMigrator;

namespace MeetingReservation.Infrastructure.Migrations.Versions;

[Migration(2, "Create Reservation table")]
public class Version000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Reservations")
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("InitialTime").AsDateTime().NotNullable()
            .WithColumn("EndTime").AsDateTime().NotNullable()
            .WithColumn("Participants").AsInt32().NotNullable()
            .WithColumn("UpdatedAt").AsDateTime().Nullable()
            .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("FK_Reservations_User_Id", "Users", "Id")
                .OnDeleteOrUpdate(System.Data.Rule.Cascade);
    }
}
