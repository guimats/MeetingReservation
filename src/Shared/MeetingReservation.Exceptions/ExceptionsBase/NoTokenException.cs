using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using System.Net;

namespace MeetingReservation.Communication.Responses;

public class NoTokenException : MeetingReservationException
{
    public NoTokenException() : base(ResourceMessagesException.NO_TOKEN) { }

    public override IList<string> GetMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
