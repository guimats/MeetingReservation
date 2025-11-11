using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Reservation.Update;

public interface IUpdateReservationUseCase
{
	public Task<ResponseShortReservationJson> Execute(RequestReservationJson request, long reservationID);
}
