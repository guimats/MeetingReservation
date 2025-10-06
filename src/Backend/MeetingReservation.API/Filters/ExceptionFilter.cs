using MeetingReservation.Communication.Responses;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeetingReservation.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is MeetingReservationException meetingReservation)
                HandleProjectException(context, meetingReservation);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(ExceptionContext context, MeetingReservationException meetingReservation)
        {
            context.HttpContext.Response.StatusCode = (int)meetingReservation.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(meetingReservation.GetMessages()));
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
            }
        }
    }
}
