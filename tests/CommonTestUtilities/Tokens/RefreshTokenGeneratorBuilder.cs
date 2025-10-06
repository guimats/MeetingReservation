using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Infrastructure.Security.Tokens.Refresh;

namespace CommonTestUtilities.Tokens;

public class RefreshTokenGeneratorBuilder
{
    public static IRefreshTokenGenerator Build()
    {
        return new RefreshTokenGenerator();
    }
}
