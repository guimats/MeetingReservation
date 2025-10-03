using MeetingReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingReservation.Infrastructure.DataAccess;

public class MeetingReservationDbContext : DbContext
{
    public MeetingReservationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingReservationDbContext).Assembly);
    }
}
