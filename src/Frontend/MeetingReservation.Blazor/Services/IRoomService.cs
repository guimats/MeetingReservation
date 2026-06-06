using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Blazor.Services;

public interface IRoomService
{
    Task<List<ResponseShortRoomJson>> FilterRoomsAsync(RequestFilterRoomsJson filter);
    Task<bool> CreateRoomAsync(RequestRoomJson request);
    Task<bool> DeleteRoomAsync(long id);
}
