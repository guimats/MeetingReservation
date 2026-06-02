using MeetingReservation.Domain.DTOs;
using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Reservation;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class ReservationRepository : IReservationWriteOnlyRepository, IReservationReadOnlyRepository, IReservationUpdateOnlyRepository
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

    async Task<Reservation?> IReservationReadOnlyRepository.GetById(long id)
    {
        return await _dbContext
            .Reservations
			.Include(res => res.User)
			.Include(res => res.Room)
            .AsNoTracking()
            .FirstOrDefaultAsync(res => res.Id == id && res.Active);
    }

	async Task<Reservation?> IReservationUpdateOnlyRepository.GetById(long reservationID, long userID)
	{
		return await _dbContext
			.Reservations
			.FirstOrDefaultAsync(res => res.Id == reservationID && res.Active && res.UserId == userID);
	}

	public void Update(Reservation reservation)
	{
		_dbContext.Reservations.Update(reservation);
	}

	public async Task<bool> IsTimeOccupied(long roomId, DateTime initialTime, DateTime endTime)
	{
		return await _dbContext.Reservations
			.Where(res => res.RoomId == roomId)
			.AnyAsync(res => initialTime < res.EndTime && endTime > res.InitialTime);
	}

	public async Task<IList<Reservation>> Filter(FilterReservationsDTO filter)
	{
		var query = _dbContext.Reservations.Where(res => res.Active);

		if (string.IsNullOrWhiteSpace(filter.Name) is false)
			query = query.Where(res => res.Name.Contains(filter.Name));
		
		if (string.IsNullOrWhiteSpace(filter.Description) is false)
			query = query.Where(res => res.Description.Contains(filter.Description));

		if (filter.MinParticipants > 0)
			query = query.Where(res => res.Participants >= filter.MinParticipants);

		return await query
			.Include(res => res.User)
			.Include(res => res.Room)
			.AsNoTracking()
			.ToListAsync();
	}
}
