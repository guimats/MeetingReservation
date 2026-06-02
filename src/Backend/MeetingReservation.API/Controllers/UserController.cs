using MeetingReservation.API.Attributes;
using MeetingReservation.Application.UseCases.User.ChangePassword;
using MeetingReservation.Application.UseCases.User.Delete;
using MeetingReservation.Application.UseCases.User.Filter;
using MeetingReservation.Application.UseCases.User.GetById;
using MeetingReservation.Application.UseCases.User.Profile;
using MeetingReservation.Application.UseCases.User.Register;
using MeetingReservation.Application.UseCases.User.Update;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [AuthenticatedUser]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [AuthenticatedUser]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileById(
        [FromServices] IGetProfileByIdUseCase useCase,
        [FromRoute] long id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }

    [AuthenticatedUser]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(
        [FromServices] IDeleteUserUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [AuthenticatedUser]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
            [FromServices] IUpdateUserUseCase useCase,
            [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [AuthenticatedUser]
    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase useCase,
            [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }

    [AuthenticatedUser]
    [HttpPost("filter")]
	[ProducesResponseType(typeof(ResponseUsersJson), StatusCodes.Status200OK)]
	public async Task<IActionResult> Filter(
		[FromServices] IFilterUsersUseCase useCase,
		[FromBody] RequestFilterUsersJson request)
	{
		var result = await useCase.Execute(request);

		return Ok(result);
	}
}
