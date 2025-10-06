namespace MeetingReservation.Application.UseCases.User.Delete;

public interface IDeleteUserUseCase
{
    public Task Execute(long id);
}
