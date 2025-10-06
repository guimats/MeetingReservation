using Bogus;
using MeetingReservation.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestLoginJsonBuilder
{
    public static RequestLoginJson Build(int passwordLength = 10)
    {
        return new Faker<RequestLoginJson>()
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email())
            .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
    }
}
