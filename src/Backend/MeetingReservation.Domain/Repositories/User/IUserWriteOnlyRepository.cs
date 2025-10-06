namespace MeetingReservation.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task Add(Entities.User user);
    public Task Delete(long id);
}
