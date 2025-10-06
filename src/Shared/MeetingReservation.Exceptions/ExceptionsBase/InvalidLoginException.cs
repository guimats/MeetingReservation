using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : MeetingReservationException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID) { }

        public override IList<string> GetMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }

}
