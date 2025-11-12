using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Room.Register;

public interface IRegisterRoomUseCase
{
	public Task Execute(RequestRoomJson request);
}
