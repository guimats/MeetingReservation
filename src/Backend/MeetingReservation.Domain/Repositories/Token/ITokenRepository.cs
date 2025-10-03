using MeetingReservation.Domain.Entities;

namespace MeetingReservation.Domain.Repositories.Token;

public interface ITokenRepository
{
    public Task<RefreshToken?> Get(string refreshToken);

    public Task SaveNewRefreshToken(RefreshToken refreshToken);

}
