namespace MeetingReservation.Application.UseCases.Room.Delete;

public interface IDeleteRoomUseCase
{
	public Task Execute(long roomId);
}
