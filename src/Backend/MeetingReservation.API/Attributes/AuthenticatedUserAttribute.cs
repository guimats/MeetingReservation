using MeetingReservation.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
