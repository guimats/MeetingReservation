using MeetingReservation.Domain.DTOs;
using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Room;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class RoomRepository : IRoomReadOnlyRepository, IRoomWriteOnlyRepository, IRoomUpdateOnlyRepository
{
	private readonly MeetingReservationDbContext _dbContext;

	public RoomRepository(MeetingReservationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	async Task<Room?> IRoomReadOnlyRepository.GetById(long roomId)
	{
		return await _dbContext.Rooms
			.AsNoTracking()
			.Include(r => r.Reservations)
			.FirstOrDefaultAsync(r => r.Id.Equals(roomId));
	}

	async Task<Room?> IRoomUpdateOnlyRepository.GetByID(long roomId, long userId)
	{
		return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id.Equals(roomId) && r.UserId.Equals(userId));
	}

	public async Task Add(Room room)
	{
		await _dbContext.Rooms.AddAsync(room);
	}

	public async Task Delete(long roomId, long userId)
	{
		var room = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id.Equals(roomId) && r.UserId.Equals(userId));

		if (room is null)
			return;

		_dbContext.Rooms.Remove(room);
	}

	public void Update(Room room)
	{
		_dbContext.Rooms.Update(room);
	}

	public async Task<IList<Room>> Filter(FilterRoomsDTO filter)
	{
		var query = _dbContext.Rooms
			.Where(r => r.Active);

		if (string.IsNullOrWhiteSpace(filter.Name) == false)
			query = query.Where(r => r.Name.Contains(filter.Name));

		if (filter.Capacity.HasValue)
			query = query.Where(r => r.Capacity >= filter.Capacity);

		if (string.IsNullOrWhiteSpace(filter.Location) == false)
			query = query.Where(r => r.Location.Contains(filter.Location));

		return await query.AsNoTracking().ToListAsync();
	}
}
