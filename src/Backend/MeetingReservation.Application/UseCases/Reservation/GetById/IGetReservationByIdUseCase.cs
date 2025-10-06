using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Application.UseCases.Reservation.GetById;

public interface IGetReservationByIdUseCase
{
    public Task<ResponseLongReservationJson> Execute(long userId);
}
