using MeetingReservation.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositorioes;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var mock = new Mock<IUserWriteOnlyRepository>();

        return mock.Object;
    }
}
