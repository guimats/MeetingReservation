using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Infrastructure.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        return new JwtTokenGenerator(expirationTimeMinutes: 10, signingKey: "tttttttttttttttttttttttttttttttt");
    }
}
