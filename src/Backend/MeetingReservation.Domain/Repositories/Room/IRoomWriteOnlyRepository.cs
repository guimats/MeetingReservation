namespace MeetingReservation.Domain.Repositories.Room;

public interface IRoomWriteOnlyRepository
{
	public Task Add(Entities.Room room);
	public Task Delete(long roomId, long userId);
}
