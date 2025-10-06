namespace MeetingReservation.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveEmail(string email);

    public Task<Entities.User?> GetByEmail(string email);

    public Task<Entities.User?> GetById(long id);

    public Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
}
