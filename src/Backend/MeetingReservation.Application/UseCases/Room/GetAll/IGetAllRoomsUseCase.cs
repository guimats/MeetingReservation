using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Room.GetAll;

public interface IGetAllRoomsUseCase
{
	public Task<ResponseAllRoomsJson> Execute();
}
