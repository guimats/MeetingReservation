using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Web.Client.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> LoginAsync(RequestLoginJson request);
        public Task LogoutAsync();
    }
}
