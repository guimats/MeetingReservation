using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Reservation.Filter;

public interface IFilterReservationsUseCase
{
	public Task<ResponseReservationsJson> Execute(RequestFilterReservationsJson request);
}
