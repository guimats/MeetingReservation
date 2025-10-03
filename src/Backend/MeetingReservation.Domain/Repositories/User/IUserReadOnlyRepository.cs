namespace MeetingReservation.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistActiveEmail(string email);
}
