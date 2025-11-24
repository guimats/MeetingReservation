using MeetingReservation.Application.UseCases.Company.Register;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
	[HttpPost]
	[ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
	public async Task<IActionResult> Register(
		[FromServices] IRegisterCompanyUseCase useCase,
		[FromBody] RequestRegisterCompanyJson request)
	{
		var result = await useCase.Execute(request);

		return Created(string.Empty, result);
	}

	[HttpGet]
	[ProducesResponseType(typeof(ResponseShortCompanyJson), StatusCodes.Status200OK)]
	public async Task<IActionResult> Register()
	{
		throw new NotImplementedException();
	}
}
