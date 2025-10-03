using MeetingReservation.Domain.Security.Tokens;

namespace MeetingReservation.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAcessor;

    public HttpContextTokenValue(IHttpContextAccessor contextAcessor)
    {
        _contextAcessor = contextAcessor;
    }

    public string Value()
    {
        var authentication = _contextAcessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authentication["Bearer ".Length..].Trim();
    }
}
