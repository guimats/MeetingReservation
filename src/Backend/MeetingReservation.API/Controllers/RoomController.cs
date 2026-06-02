using MeetingReservation.API.Attributes;
using MeetingReservation.Application.UseCases.Room.Delete;
using MeetingReservation.Application.UseCases.Room.Filter;
using MeetingReservation.Application.UseCases.Room.GetById;
using MeetingReservation.Application.UseCases.Room.Register;
using MeetingReservation.Application.UseCases.Room.Update;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;

//[AuthenticatedUser]
[Route("[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Register(
		[FromServices] IRegisterRoomUseCase useCase,
		[FromBody] RequestRoomJson request)
	{
		await useCase.Execute(request);

		return NoContent();
	}

	[HttpGet("{id}")]
	[ProducesResponseType(typeof(ResponseRoomJson), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetById(
		[FromServices] IGetRoomByIdUseCase useCase,
		[FromRoute] long id)
	{
		var result = await useCase.Execute(id);

		return Ok(result);
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Delete(
		[FromServices] IDeleteRoomUseCase useCase,
		[FromRoute] long id)
	{
		await useCase.Execute(id);

		return NoContent();
	}

	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<IActionResult> Update(
		[FromServices] IUpdateRoomUseCase useCase,
		[FromBody] RequestRoomJson request,
		[FromRoute] long id)
	{
		await useCase.Execute(request, id);

		return NoContent();
	}

	[HttpPost("filter")]
	[ProducesResponseType(typeof(ResponseRoomsJson), StatusCodes.Status200OK)]
	public async Task<IActionResult> Filter(
		[FromServices] IFilterRoomsUseCase useCase,
		[FromBody] RequestFilterRoomsJson request)
	{
		var result = await useCase.Execute(request);

		return Ok(result);
	}
}
