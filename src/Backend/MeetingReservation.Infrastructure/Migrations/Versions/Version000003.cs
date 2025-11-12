using FluentMigrator;
using System.Data;

namespace MeetingReservation.Infrastructure.Migrations.Versions
{
	[Migration(3, "Create Room table and updating Reservation table")]
	public class Version000003 : VersionBase
	{
		public override void Up()
		{
			CreateTable("Rooms")
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Capacity").AsInt32().NotNullable()
				.WithColumn("Location").AsString(255).Nullable()
				.WithColumn("UserId").AsInt32().NotNullable();

			Alter.Table("Reservations")
				.AddColumn("RoomId").AsInt64().NotNullable()
				.SetExistingRowsTo(1);
									  
			Create.ForeignKey("FK_Reservations_Room_Id")
				.FromTable("Reservations").InSchema("public").ForeignColumn("RoomId")
				.ToTable("Rooms").InSchema("public").PrimaryColumn("Id")
				.OnDeleteOrUpdate(Rule.Cascade);
		}
	}
}
