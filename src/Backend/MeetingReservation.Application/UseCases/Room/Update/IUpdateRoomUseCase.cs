using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Room.Update;

public interface IUpdateRoomUseCase
{
	public Task Execute(RequestRoomJson request, long roomId);
}
