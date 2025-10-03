using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Token;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly MeetingReservationDbContext _dbContext;

    public TokenRepository(MeetingReservationDbContext dbContext) => _dbContext = dbContext;

    public async Task<RefreshToken?> Get(string refreshToken)
    {
        return await _dbContext
                .RefreshTokens
                .AsNoTracking()
                .Include(token => token.User)
                .FirstOrDefaultAsync(token => token.Value.Equals(refreshToken));
    }

    public async Task SaveNewRefreshToken(RefreshToken refreshToken)
    {
        var tokens = _dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);

        _dbContext.RefreshTokens.RemoveRange(tokens);

        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }
}
