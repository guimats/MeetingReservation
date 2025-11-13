using FluentMigrator;

namespace MeetingReservation.Infrastructure.Migrations.Versions;

[Migration(4, "Create Company Table")]
public class Version000004 : VersionBase
{
	public override void Up()
	{
		CreateTable("Companies")
			.WithColumn("Name").AsString(255).NotNullable();
	}
}
