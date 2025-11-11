using Bogus;
using MeetingReservation.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestUpdateUserJsonBuilder
{
	public static RequestUpdateUserJson Build(int passwordLength = 10)
	{
		return new Faker<RequestUpdateUserJson>()
			.RuleFor(user => user.Name, (f) => f.Person.FirstName)
			.RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
	}
}
