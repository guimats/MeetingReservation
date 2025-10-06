using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.User.ChangePassword;

public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordJson request);
}
