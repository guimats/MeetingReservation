using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Token;
using Moq;

namespace CommonTestUtilities.Repositorioes;

public class TokenRepositoryBuilder
{
    private readonly Mock<ITokenRepository> _repository;

    public TokenRepositoryBuilder() => _repository = new Mock<ITokenRepository>();

    public TokenRepositoryBuilder Get(RefreshToken? refreshToken = null)
    {
        if (refreshToken is not null)
            _repository.Setup(repository => repository.Get(refreshToken.Value)).ReturnsAsync(refreshToken);

        return this;
    }

    public ITokenRepository Build() => _repository.Object;
}
