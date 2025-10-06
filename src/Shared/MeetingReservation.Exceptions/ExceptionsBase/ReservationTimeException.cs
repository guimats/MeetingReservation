using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase;

public class ReservationTimeException : MeetingReservationException
{
    public ReservationTimeException() : base(ResourceMessagesException.INITIAL_TIME_MUST_BE_EARLIER) { }
    public override IList<string> GetMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
