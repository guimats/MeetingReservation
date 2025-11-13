using MeetingReservation.Domain.DTOs;

namespace MeetingReservation.Domain.Repositories.Room;

public interface IRoomReadOnlyRepository
{
	public Task<Entities.Room?> GetById(long roomId);

	public Task<IList<Entities.Room>> Filter(FilterRoomsDTO filter);
}
