using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase;

public class NotFoundException : MeetingReservationException
{
    public NotFoundException(string message) : base(message) { }
    public override IList<string> GetMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
