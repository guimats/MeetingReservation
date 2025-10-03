using MeetingReservation.Communication.Requests;

namespace MeetingReservation.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task Execute(RequestLoginJson request);
}
