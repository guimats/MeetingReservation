using MeetingReservation.Domain.Security.Cryptography;
using MeetingReservation.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new BCryptNet();
}
