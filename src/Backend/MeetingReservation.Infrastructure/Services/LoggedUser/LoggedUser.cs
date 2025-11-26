using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Security.Tokens;
using MeetingReservation.Domain.Services.LoggedUser;
using MeetingReservation.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeetingReservation.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly MeetingReservationDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(MeetingReservationDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jetSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jetSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.Users.IgnoreQueryFilters().AsNoTracking().FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
    }
}
