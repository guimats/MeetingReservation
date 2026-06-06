using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Blazor.Services;

public interface IReservationService
{
    Task<List<ResponseShortReservationJson>> FilterReservationsAsync(RequestFilterReservationsJson filter);
    Task<bool> CreateReservationAsync(RequestReservationJson request);
    Task<bool> DeleteReservationAsync(long id);
}
