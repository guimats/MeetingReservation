using MeetingReservation.Application.UseCases.Token;
using MeetingReservation.Communication.Requests;
using MeetingReservation.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MeetingReservation.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(
            [FromServices] IUseRefreshTokenUseCase useCase,
            [FromBody] RequestNewTokenJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
