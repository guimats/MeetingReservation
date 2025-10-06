using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase;

public class UserNotExistException : MeetingReservationException
{
    public UserNotExistException() : base(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE) { }

    public override IList<string> GetMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
