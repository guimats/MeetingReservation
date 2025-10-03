using MeetingReservation.Domain.Security.Tokens;

namespace MeetingReservation.Infrastructure.Security.Tokens.Refresh;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate() => Convert.ToBase64String(Guid.NewGuid().ToByteArray());
}
