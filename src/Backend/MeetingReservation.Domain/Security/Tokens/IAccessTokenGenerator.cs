using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string Generate(User user);
}
