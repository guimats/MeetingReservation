using MeetingReservation.Domain.Repositories;

namespace MeetingReservation.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MeetingReservationDbContext _dbContext;

        public UnitOfWork(MeetingReservationDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }

}
