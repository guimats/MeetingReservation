using MeetingReservation.API.Attributes;
using MeetingReservation.Application.UseCases.Reservation.GetById;
using MeetingReservation.Application.UseCases.Reservation.Register;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;


[AuthenticatedUser]
[Route("[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortReservationJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterReservationUseCase useCase,
        [FromBody] RequestReservationJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseLongReservationJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetReservationByIdUseCase useCase,
        [FromRoute] long id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }
}
