using MeetingReservation.Domain.Enums;

namespace MeetingReservation.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string Generate(Guid userIdentifier, Role role);
}
