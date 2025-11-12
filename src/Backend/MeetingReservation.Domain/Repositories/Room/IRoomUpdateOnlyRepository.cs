namespace MeetingReservation.Domain.Repositories.Room;

public interface IRoomUpdateOnlyRepository
{
	public void Update(Entities.Room room);

	public Task<Entities.Room?> GetByID(long roomId, long userId);
}
