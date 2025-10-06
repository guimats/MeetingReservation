using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositorioes;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository;

    public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

    public void ExistActiveEmail(string email)
    {
        _repository.Setup(repository => repository.ExistActiveEmail(email)).ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetByEmail(User? user = null)
    {
        if (user is not null)
            _repository.Setup(re => re.GetByEmail(user.Email)).ReturnsAsync(user);

        return this;
    }

    public UserReadOnlyRepositoryBuilder GetById(User user)
    {
        if (user is not null)
            _repository.Setup(re => re.GetById(user.Id)).ReturnsAsync(user);

        return this;
    }

    public IUserReadOnlyRepository Build() => _repository.Object;
}
