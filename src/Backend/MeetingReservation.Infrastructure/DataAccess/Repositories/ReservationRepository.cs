using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Reservation;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class ReservationRepository : IReservationWriteOnlyRepository, IReservationReadOnlyRepository
{
    private readonly MeetingReservationDbContext _dbContext;

    public ReservationRepository(MeetingReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Reservation reservation) => await _dbContext.Reservations.AddAsync(reservation);

    public async Task Delete(long id)
    {
        var reservation = await _dbContext.Reservations.FindAsync(id);

        if (reservation is null)
            return;

        _dbContext.Reservations.Remove(reservation);
    }

    public async Task<Reservation?> GetById(long id)
    {
        return await _dbContext
            .Reservations
            .AsNoTracking()
            .FirstOrDefaultAsync(res => res.Id == id && res.Active);
    }
}
