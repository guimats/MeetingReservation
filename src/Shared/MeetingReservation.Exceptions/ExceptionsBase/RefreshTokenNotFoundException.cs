using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase;

public class RefreshTokenNotFoundException : MeetingReservationException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.NO_TOKEN) {}

    public override IList<string> GetMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
