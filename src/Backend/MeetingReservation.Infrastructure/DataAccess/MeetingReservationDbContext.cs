using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Services.TenantProvider;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess;

public class MeetingReservationDbContext : DbContext
{
	private readonly ITenantProvider _tenantProvider;

	public MeetingReservationDbContext(DbContextOptions options, ITenantProvider tenantProvider) : base(options)
	{
		_tenantProvider = tenantProvider;
	}

    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
	public DbSet<Room> Rooms { get; set; }
    public DbSet<Company> Companies { get; set; }

	public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingReservationDbContext).Assembly);

		modelBuilder.Entity<User>().HasQueryFilter(u => u.CompanyId == _tenantProvider.GetCompanyId());
		modelBuilder.Entity<Reservation>().HasQueryFilter(r => r.CompanyId == _tenantProvider.GetCompanyId());
		modelBuilder.Entity<Room>().HasQueryFilter(r => r.CompanyId == _tenantProvider.GetCompanyId());
	}
}
