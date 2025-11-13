using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Room.Filter;

public interface IFilterRoomsUseCase
{
	public Task<ResponseRoomsJson> Execute(RequestFilterRoomsJson request);
}
