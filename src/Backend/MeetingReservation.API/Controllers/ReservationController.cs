using Azure.Core;
using MeetingReservation.API.Attributes;
using MeetingReservation.Application.UseCases.Reservation.Delete;
using MeetingReservation.Application.UseCases.Reservation.Filter;
using MeetingReservation.Application.UseCases.Reservation.GetById;
using MeetingReservation.Application.UseCases.Reservation.Register;
using MeetingReservation.Application.UseCases.Reservation.Update;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;


//[AuthenticatedUser]
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteReservationUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

	[HttpPut("{id}")]
	[ProducesResponseType(typeof(ResponseShortReservationJson), StatusCodes.Status200OK)]
	public async Task<IActionResult> Update(
		[FromServices] IUpdateReservationUseCase useCase,
		[FromBody] RequestReservationJson request,
		[FromRoute] long id)
	{
		var result = await useCase.Execute(request, id);

		return Ok(result);
	}

    [HttpPost("filter")]
    [ProducesResponseType(typeof(ResponseReservationsJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Filter(
        [FromServices] IFilterReservationsUseCase useCase,
        [FromBody] RequestFilterReservationsJson request
        )
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
