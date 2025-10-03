using System.Net;

namespace MeetingReservation.Exceptions.ExceptionsBase;

public abstract class MeetingReservationException : SystemException
{
    public MeetingReservationException(string message) : base(message) { }
    
    public abstract HttpStatusCode GetStatusCode();

    public abstract IList<string> GetMessages();
}
