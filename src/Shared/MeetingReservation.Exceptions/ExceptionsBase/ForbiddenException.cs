using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeetingReservation.Exceptions.ExceptionsBase
{
    public class ForbiddenException : MeetingReservationException
    {
        public ForbiddenException() : base(ResourceMessagesException.FORBIDDEN_ACCESS) { }

        public override IList<string> GetMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }

}
