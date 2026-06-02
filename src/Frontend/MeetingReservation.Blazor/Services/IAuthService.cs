using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Blazor.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(RequestLoginJson request);
    Task LogoutAsync();
}
