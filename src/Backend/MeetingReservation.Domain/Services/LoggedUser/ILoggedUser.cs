using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    public Task<User> User();
}
