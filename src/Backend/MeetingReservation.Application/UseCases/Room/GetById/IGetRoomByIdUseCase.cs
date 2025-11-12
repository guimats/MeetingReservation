using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Room.GetById;

public interface IGetRoomByIdUseCase
{
	public Task<ResponseRoomJson> Execute(long roomId);
}
