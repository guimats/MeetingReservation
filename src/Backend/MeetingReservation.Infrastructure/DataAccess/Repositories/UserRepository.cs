using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MeetingReservationDbContext _dbContext;

    public UserRepository(MeetingReservationDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
   
    public async Task<bool> ExistActiveEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email == email && user.Active);

    public async Task<User?> GetByEmail(string email) 
    {
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Active);
    } 

}
