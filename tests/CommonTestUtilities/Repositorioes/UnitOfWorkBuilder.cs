using Moq;
using MeetingReservation.Domain.Repositories;

namespace CommonTestUtilities.Repositorioes;

public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}
