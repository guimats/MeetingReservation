using FluentMigrator;
using System.Data;

namespace MeetingReservation.Infrastructure.Migrations.Versions
{
    [Migration(1, "Create initial tables and rules")]
    public class Version000001 : VersionBase
    {
        public override void Up()
        {
			// Criando Companies (tabela base)
			CreateTable("Companies")
				.WithColumn("Name").AsString(255).NotNullable();

			// Criando table Users
			CreateTable("Users")
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Email").AsString(255).NotNullable().Unique()
				.WithColumn("Password").AsString(2000).NotNullable()
				.WithColumn("UserIdentifier").AsGuid().NotNullable().Unique()
				.WithColumn("Role").AsInt32().NotNullable()
				.WithColumn("CompanyId").AsInt64().NotNullable()
					.ForeignKey("FK_Users_Companies_Id", "Companies", "Id")
					.OnDelete(Rule.Cascade)
					.Indexed();

			// Criando tabela rooms
			CreateTable("Rooms")
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Capacity").AsInt32().NotNullable()
				.WithColumn("Location").AsString(255).Nullable()
				.WithColumn("UserId").AsInt64().NotNullable()
					.ForeignKey("FK_Rooms_Users_Id", "Users", "Id")
				.WithColumn("CompanyId").AsInt64().NotNullable()
					.ForeignKey("FK_Rooms_Companies_Id", "Companies", "Id")
					.OnDelete(Rule.Cascade);

			// Criando tabela reservations
			CreateTable("Reservations")
				.WithColumn("Name").AsString(255).NotNullable()
				.WithColumn("Description").AsString(500).Nullable()
				.WithColumn("InitialTime").AsDateTime().NotNullable()
				.WithColumn("EndTime").AsDateTime().NotNullable()
				.WithColumn("Participants").AsInt32().NotNullable()
				.WithColumn("UpdatedAt").AsDateTime().Nullable()
				.WithColumn("UserId").AsInt64().NotNullable()
					.ForeignKey("FK_Reservations_Users_Id", "Users", "Id")
					.OnDelete(Rule.Cascade)
				.WithColumn("RoomId").AsInt64().NotNullable()
					.ForeignKey("FK_Reservations_Rooms_Id", "Rooms", "Id")
					.OnDelete(Rule.Cascade)
				.WithColumn("CompanyId").AsInt64().NotNullable()
					.ForeignKey("FK_Reservations_Companies_Id", "Companies", "Id")
					.OnDelete(Rule.None); // Evitar ciclos de delete em cascata multiplos (Company->Room->Reservation vs Company->Reservation)

			// 5. Criando tabela de refresh tokens
			CreateTable("RefreshTokens")
				.WithColumn("Value").AsString(int.MaxValue).NotNullable()
				.WithColumn("UserId").AsInt64().NotNullable()
					.ForeignKey("FK_RefreshTokens_Users_Id", "Users", "Id")
					.OnDelete(Rule.Cascade);
		}
    }
}
