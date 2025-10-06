using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    public Task Execute(RequestUpdateUserJson request);
}
