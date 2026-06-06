using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;

namespace MeetingReservation.Blazor.Services;

public interface IUserService
{
    Task<List<ResponseUserProfileJson>> FilterUsersAsync(RequestFilterUsersJson filter);
    Task<bool> CreateUserAsync(RequestRegisterUserJson request);
    Task<bool> DeleteUserAsync(long id);
}
