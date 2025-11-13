using MeetingReservation.Domain.DTOs;
using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly MeetingReservationDbContext _dbContext;

    public UserRepository(MeetingReservationDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task Delete(long id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user is null)
            return;

        var refreshTokens = _dbContext.RefreshTokens.Where(rt => rt.UserId == id);

        _dbContext.RefreshTokens.RemoveRange(refreshTokens);

        _dbContext.Users.Remove(user!);
    }

    public async Task<bool> ExistActiveEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
    {
        return await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
    }

    public async Task<User?> GetByEmail(string email) 
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Active);
    }

    async Task<User?> IUserReadOnlyRepository.GetById(long id) => await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id.Equals(id) && user.Active);

    async Task<User?> IUserUpdateOnlyRepository.GetById(long id) => await _dbContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(id) && user.Active);

    public void Update(User user) => _dbContext.Users.Update(user);

	public async Task<IList<User>> Filter(FilterUsersDTO filter)
	{
        var query = _dbContext.Users.Where(u => u.Active);

        if (string.IsNullOrWhiteSpace(filter.Name) == false)
        {
            query = query.Where(u => u.Name.Contains(filter.Name));
        }

        if (string.IsNullOrWhiteSpace(filter.Email) == false){
            query = query.Where(u => u.Email.StartsWith(filter.Email));
        }

        if (filter.Role is not null)
        {
            query = query.Where(u => u.Role.Equals(filter.Role));
        }

        return await query.AsNoTracking().ToListAsync();
	}
}
