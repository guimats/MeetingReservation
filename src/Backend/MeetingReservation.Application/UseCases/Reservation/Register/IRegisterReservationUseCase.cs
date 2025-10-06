using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Reservation.Register;

public interface IRegisterReservationUseCase
{
    public Task<ResponseShortReservationJson> Execute(RequestReservationJson request);
}
